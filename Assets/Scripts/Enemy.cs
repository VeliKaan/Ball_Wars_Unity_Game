using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed; // Movement speed of the enemy

    private Rigidbody _rigidbody; // Reference to the Rigidbody component
    private GameObject _player; // Reference to the player object

    void Start()
    {
        // Find the player GameObject in the scene
        _player = GameObject.Find("Player");

        // Get the Rigidbody component attached to the enemy
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move towards the player if the player exists
        if (_player != null)
        {
            // Calculate the direction vector from enemy to player
            Vector3 lookDirection = (_player.transform.position - transform.position).normalized;

            // Apply force to move the enemy towards the player
            _rigidbody.AddForce(lookDirection * Speed);
        }

        // Destroy the enemy if it falls below a certain height
        if (transform.position.y < -10.0f)
        {
            Destroy(gameObject);
        }
    }
}
