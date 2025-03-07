using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class Menu : MonoBehaviour
{
    // This function will be called when the Play button is clicked
    public void PlayGame()
    {
        // Load the game scene (replace "Ball_Wars" with your actual game scene name)
        SceneManager.LoadScene("Ball_Wars");
    }

    // This function will be called when the Settings button is clicked
    public void OpenSettings()
    {
        // You can load a settings scene or display settings options here
        // Example: SceneManager.LoadScene("SettingsScene"); 
        Debug.Log("Settings opened");
    }

    // This function will be called when the Exit button is clicked
    public void QuitGame()
    {
        // This will close the application
        Debug.Log("Game is closing...");
        Application.Quit();

        // If you are testing in the Unity Editor, this will stop play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

