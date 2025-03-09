using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))    // quit game if user press x
        {
            Debug.Log("X Key Pressed: Quitting Game!");
            QuitGame();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Triggered!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
