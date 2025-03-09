using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Animator transition; // Reference to the Animator
    public float transitionTime = 1f; // Duration of the fade effect

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        transition.SetTrigger("StartFade"); // Triggers FadeOut
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("EndFade"); // Triggers FadeIn after loading
    }
}
