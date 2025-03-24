using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject menuUI; // Assign the Pause Menu UI
    public GameObject player; // Assign the Player GameObject
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Press ESC to toggle menu
        {
            Debug.Log("Esc Button Clicked!");
            if (isPaused)
            {
                ResumeGame();
            }

            // modification: don't overlap pause when another menu is present
            else if (Time.timeScale > 0)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        menuUI.SetActive(true); // Show menu
        Time.timeScale = 0f; // Pause game physics
        DisablePlayerControls();
        Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI
        Cursor.visible = true; // Make cursor visible
        isPaused = true;
    }

    public void ResumeGame()
    {
        menuUI.SetActive(false); // Hide menu
        Time.timeScale = 1f; // Resume game
        EnablePlayerControls();
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor back to game
        Cursor.visible = false; // Hide cursor when resuming
        isPaused = false;
    }

    public void RestartLevel()
    {
        Debug.Log("Restart Clicked!");
        Time.timeScale = 1f; // Reset time scale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void QuitGame()
    {
        Debug.Log("Quit Button Clicked!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in Unity Editor
#else
            Application.Quit(); // Quit game in a built version
#endif
    }

    void DisablePlayerControls()
    {
        if (player != null)
        {
            if (player.GetComponent<MovementStateController>() != null)
                player.GetComponent<MovementStateController>().enabled = false;

            if (player.GetComponent<CharacterController>() != null)
                player.GetComponent<CharacterController>().enabled = false;
        }
    }

    void EnablePlayerControls()
    {
        if (player != null)
        {
            if (player.GetComponent<MovementStateController>() != null)
                player.GetComponent<MovementStateController>().enabled = true;

            if (player.GetComponent<CharacterController>() != null)
                player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
