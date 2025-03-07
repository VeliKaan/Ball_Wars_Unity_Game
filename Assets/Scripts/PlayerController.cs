using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 5.0f; // Player movement speed
    private bool _hasPowerup = false; // Tracks if the player has a powerup
    private PowerType _powerType; // Stores the current powerup type
    private static readonly float _powerupStrength = 15.0f; // Strength applied to enemies when using powerups

    private Rigidbody _rigidbody;
    private GameObject _focalPoint; // Object that determines player movement direction
    private GameObject _powerupIndicator; // UI element indicating active powerup

    public GameObject ProjectilePrefab; // Prefab for the projectile attack

    private enum SmashState
    {
        None, // No smashing action
        Up,   // Player is moving up for a smash
        Down  // Player is smashing down
    };

    private SmashState _smashState = SmashState.None;
    private Vector3 _smashPosition;
    private static readonly float _smashDuration = 0.3f; // Time taken to complete a smash move
    private static readonly float _smashSpeed = 25.0f;   // Speed of the smash movement
    private static readonly float _smashForce = 250.0f;  // Force applied to enemies when smashing

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("Focal Point");
        _powerupIndicator = GameObject.Find("Powerup Indicator");
        _powerupIndicator.SetActive(false); // Hide powerup indicator at start
    }

    void Update()
    {
        if (_smashState == SmashState.None)
        {
            // Get player input and move in the direction of the focal point
            float forwardInput = Input.GetAxis("Vertical");
            _rigidbody.AddForce(_focalPoint.transform.forward * (Speed * forwardInput));
        }
        else
        {
            // Handle smash movement
            float direction = _smashState == SmashState.Up ? 1 : -1;
            _smashPosition += new Vector3(0, Time.deltaTime * direction * _smashSpeed, 0);
            transform.position = _smashPosition;
        }

        // Position the powerup indicator slightly below the player
        _powerupIndicator.transform.position = transform.position + new Vector3(0, -0.25f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            // Stop any existing powerup effects before applying a new one
            StopCoroutine(nameof(PowerupCountdownRoutine));
            StopCoroutine(nameof(PowerupProjectileRoutine));

            var powerup = other.gameObject.GetComponent<Powerup>();
            _powerType = powerup.PowerType;

            ActivatePowerup(true); // Activate the powerup

            Destroy(other.gameObject); // Remove the powerup object

            // Activate the appropriate powerup effect
            switch (_powerType)
            {
                case PowerType.Superpower:
                    StartCoroutine(PowerupCountdownRoutine());
                    break;
                case PowerType.Projectiles:
                    StartCoroutine(PowerupProjectileRoutine());
                    break;
                case PowerType.Smash:
                    StartCoroutine(PowerupSmushRoutine());
                    break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var otherGameObject = collision.gameObject;

        // If the player has the Superpower powerup, apply force to enemies on collision
        if (otherGameObject.CompareTag("Enemy") && _hasPowerup && _powerType == PowerType.Superpower)
        {
            ForceEnemy(otherGameObject, _powerupStrength);
        }
    }

    private void ForceEnemy(GameObject enemy, float strength, bool distanceDependent = false)
    {
        var enemyRigidbody = enemy.GetComponent<Rigidbody>();
        var awayFromPlayer = enemy.transform.position - transform.position;

        float distanceK = 1.0f;
        if (distanceDependent)
        {
            // Scale force based on distance (weaker at longer distances)
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            distanceK = (float)(1.0f / Math.Pow(1.0f + distance, 2.0f));
            Debug.Log(distanceK);
        }

        var force = awayFromPlayer * (strength * distanceK);
        enemyRigidbody.AddForce(force, ForceMode.Impulse);
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        // Powerup lasts for 7 seconds
        yield return new WaitForSeconds(7);
        ActivatePowerup(false);
    }

    private IEnumerator PowerupProjectileRoutine()
    {
        // Fire projectiles three times, one per second
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1);
            ShootToEnemies();
        }
        ActivatePowerup(false);
    }

    private void ShootToEnemy(GameObject enemy)
    {
        // Instantiate a projectile and aim it at the enemy
        var projectile = Instantiate(ProjectilePrefab);
        projectile.transform.position = transform.position;
        projectile.transform.LookAt(new Vector3(enemy.transform.position.x, projectile.transform.position.y, enemy.transform.position.z));
        projectile.transform.Rotate(90, 0, 0);
    }

    private GameObject[] FindEnemies()
    {
        // Get all enemies in the scene
        return GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void ShootToEnemies()
    {
        // Shoot projectiles at all enemies
        GameObject[] enemies = FindEnemies();
        for (int i = 0; i < enemies.Length; i++)
        {
            ShootToEnemy(enemies[i]);
        }
    }

    private IEnumerator PowerupSmushRoutine()
    {
        // Perform the smash attack three times
        for (int i = 0; i < 3; i++)
        {
            _smashPosition = this.transform.position;
            _smashState = SmashState.Up;
            yield return new WaitForSeconds(_smashDuration / 2);

            void Smash()
            {
                GameObject[] enemies = FindEnemies();
                for (int i = 0; i < enemies.Length; i++)
                {
                    ForceEnemy(enemies[i], _smashForce, true);
                }
            };

            Smash();

            _smashState = SmashState.Down;
            yield return new WaitForSeconds(_smashDuration / 2);

            _smashState = SmashState.None;
            yield return new WaitForSeconds(2.0f);
        }
        ActivatePowerup(false);
    }

    private void ActivatePowerup(bool value)
    {
        _hasPowerup = value;
        _powerupIndicator.SetActive(_hasPowerup);
    }
}
