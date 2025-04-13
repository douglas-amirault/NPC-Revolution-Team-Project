using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFirstScene : MonoBehaviour
{
    public void FirstScene()
    {
        Debug.Log("Activate!");
        SceneManager.LoadScene("Intro");
    }
}
