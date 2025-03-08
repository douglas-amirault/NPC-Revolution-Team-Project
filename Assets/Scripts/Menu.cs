using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public SceneTransition sceneTransition; // Reference to scene transition

    // testing visual studio 

    public void StartGame()
    {
        Debug.Log("Start Game Button Clicked!"); // Debug message
        SceneManager.LoadScene("Level 2"); // Ensure "Level 2" is correct
        //sceneTransition.LoadScene("Level 2"); // Change this to your game scene name
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Button Clicked!");
        Application.Quit(); // Works in a built application
    }
}
