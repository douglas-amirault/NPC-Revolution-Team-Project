using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TutorialZone : MonoBehaviour
{
    public GameObject TutorialMenu;
    public string tutorialText;
    private bool tutorialActivated;

    // Start is called before the first frame update
    void Start()
    {
        tutorialActivated = false;
        TutorialMenu.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !tutorialActivated)
        {
            Time.timeScale = 0f;
            TextMeshProUGUI tutorialMenu = TutorialMenu.GetComponentInChildren<TextMeshProUGUI>();
            tutorialMenu.text = tutorialText;
            TutorialMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI
            Cursor.visible = true; // Make cursor visible
            tutorialActivated = true;
        }
    }

    public void ResumeGame()
    {
        TutorialMenu.SetActive(false); // Hide menu
        Time.timeScale = 1f; // Resume game
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor back to game
        Cursor.visible = false; // Hide cursor when resuming
    }
}
