using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private bool isPaused = false;

    void Awake()
    {
        // Get the CanvasGroup component attached to this GameObject
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component missing from StartInGameMenu!");
        }

        // Ensure the menu starts hidden
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
    // Update is called once per frame   
    void Update()
    {
        // Debug log to check if Update is running
        Debug.Log("PauseMenuToggle script is active...");

        if (Input.GetButtonUp("Cancel")) // Supports Escape & Controllers
        {
            Debug.Log("Escape Key (or controller back button) Pressed!");

            isPaused = !isPaused; // Toggle isPaused (switch between true & false)

            if (isPaused)
            {
                // Show menu & pause game
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                Time.timeScale = 0f;
            }
            else
            {
                // Hide menu & resume game
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                Time.timeScale = 1f;
            }
        }
    }

}
