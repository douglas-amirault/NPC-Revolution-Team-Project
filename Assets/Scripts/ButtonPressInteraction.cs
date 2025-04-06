using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

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
    private AudioSource audioSource;
    public AudioClip doorLevelSound;
    public GameObject winMenuScreen;

    void Start()
    {
        buttonAnimator = buttonTop.GetComponent<Animator>();
        doorAnimator = door.GetComponent<Animator>();
        buttonRenderer = buttonTop.GetComponent<Renderer>();

        string currentSceneName = SceneManager.GetActiveScene().name;
        
        if (currentSceneName == "Level 3")
        {
            //winText.gameObject.SetActive(false);
            winMenuScreen.SetActive(false);
            StartCoroutine(DelayedLightingUpdate());
        }
    }

    void Update()
    {
        // Prevent interaction if clicking UI
        //if (EventSystem.current.IsPointerOverGameObject()) return;

        float distanceToButton = Vector3.Distance(transform.position, buttonTop.transform.position);

        if (distanceToButton < interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            buttonAnimator.SetTrigger("ButtonPress");
            doorAnimator.SetTrigger("DoorOpen");
            buttonRenderer.material = glowingGreenMaterial;

            string currentSceneName = SceneManager.GetActiveScene().name;
            audioSource = gameObject.AddComponent<AudioSource>();
            if (currentSceneName == "Level 1")
            {
                audioSource.PlayOneShot(doorLevelSound);
                StartCoroutine(LoadLevelWithDelay("Level 2", 1.5f));
            }
            else if (currentSceneName == "Level 2")
            {
                audioSource.PlayOneShot(doorLevelSound);
                StartCoroutine(LoadLevelWithDelay("Level 3", 1.5f));
            }
            else if (currentSceneName == "Level 3")
            {
                audioSource.PlayOneShot(doorLevelSound);
                StartCoroutine(LoadLevelWithDelay("YouWin", 1.5f));
            }
        }
    }

    private IEnumerator LoadLevelWithDelay(string levelName, float delay)
    {
        yield return new WaitForSeconds(delay);
        // DG: see OnSceneLoaded() below and https://docs.unity3d.com/6000.0/Documentation/ScriptReference/SceneManagement.SceneManager-sceneLoaded.html
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(levelName);
    }

    private IEnumerator LoadDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // DG: there was issue where Level 2 lighting caused Level 3 lighting to act weird (i.e., be dark)
        // I added distinct light setting to level 2 & 3. Using DynamicGI.UpdateEnvironment() to fix light when it loads (see this post: https://www.reddit.com/r/Unity3D/comments/128poyx/different_lighting_in_build/)
        // OnSceneLoaded() is custom callback - see Unity docs for more: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/SceneManagement.SceneManager-sceneLoaded.html
        // Also having light issue in editor seems fairly common but the build is not impacted: https://discussions.unity.com/t/lights-get-darker-when-loading-scene/175994/3
        if (scene.name == "Level 3")
        {
            DynamicGI.UpdateEnvironment();
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator DelayedLightingUpdate()
    {
        yield return new WaitForEndOfFrame();
        DynamicGI.UpdateEnvironment();
    }
}