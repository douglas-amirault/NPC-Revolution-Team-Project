using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonPressInteraction : MonoBehaviour
{
    public GameObject buttonTop;
    public GameObject door;
    public Material glowingGreenMaterial;
    public float interactionDistance = 10;

    private Animator buttonAnimator;
    private Animator doorAnimator;
    private Renderer buttonRenderer;

    public TextMeshProUGUI winText;

    void Start()
    {
        buttonAnimator = buttonTop.GetComponent<Animator>();
        doorAnimator = door.GetComponent<Animator>();
        buttonRenderer = buttonTop.GetComponent<Renderer>();

        string currentSceneName = SceneManager.GetActiveScene().name;
        
        if (currentSceneName == "Level 3")
        {
            winText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        float distanceToButton = Vector3.Distance(transform.position, buttonTop.transform.position);

        if (distanceToButton < interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            buttonAnimator.SetTrigger("ButtonPress");
            doorAnimator.SetTrigger("DoorOpen");
            buttonRenderer.material = glowingGreenMaterial;

            string currentSceneName = SceneManager.GetActiveScene().name;

            if (currentSceneName == "Level 1")
            {
                StartCoroutine(LoadLevelWithDelay("Level 2", 1.5f));
            }
            else if (currentSceneName == "Level 2")
            {
                StartCoroutine(LoadLevelWithDelay("Level 3", 1.5f));
            }
            else if (currentSceneName == "Level 3")
            {
                winText.gameObject.SetActive(true);
                winText.text = "You Win!";
            }
        }
    }

    private IEnumerator LoadLevelWithDelay(string levelName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(levelName);
    }
}