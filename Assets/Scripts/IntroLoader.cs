using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class IntroLoader : MonoBehaviour
{

    public float autoLoadDelay = 70f; // Time in seconds before auto-loading next scene
    private float timer = 0f;
    public void Update()
    { 
        timer += Time.deltaTime;
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || timer >= autoLoadDelay)
        {
            Debug.Log("Loading Start Menu");
            SceneManager.LoadScene("Menu");
        }
    }
}
