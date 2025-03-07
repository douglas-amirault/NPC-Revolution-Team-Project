using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public SceneTransition sceneTransition; // Reference to scene transition

    public void StartGame()
    {
        sceneTransition.LoadScene("Level3"); // Change this to your game scene name
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reloads the current level
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!"); // Works in a built application
    }
}
