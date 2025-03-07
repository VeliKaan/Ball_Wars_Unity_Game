using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    // Reference to the GameManager script
    public GameManager GameManager;
    // Defines the lower boundary for the player’s position before triggering game over
    private static readonly float _lowerBound = -5.0f;


    void Update()
    {
        // Check if the player's Y position is below the lower boundary
        if (_lowerBound > transform.position.y)
        {
            Debug.Log("Game Over!");
            Destroy(gameObject);
            GameManager.GameOver = true;
            SceneManager.LoadScene("Menu");

        }
    }
}
