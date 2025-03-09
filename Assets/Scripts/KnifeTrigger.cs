using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTrigger : MonoBehaviour
{
    public AudioClip knifeSound;
    private AudioSource audioSource;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
       gameOverScreen.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) 
        {
            // Debug.Log("KnifeTrigger::OnTriggerEnter: No AudioSource component found. Dynamically adding after collision.");

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (knifeSound != null)
            {
                // Debug.Log("KnifeTrigger::OnTriggerEnter: knifeSound was NOT null");
                if (audioSource != null)
                {
                    // Debug.Log("KnifeTrigger::OnTriggerEnter: audioSource was NOT null");
                    audioSource.PlayOneShot(knifeSound);
                }
                else
                {
                    // Debug.Log("KnifeTrigger::OnTriggerEnter: audioSource was null - adding AudioSource component and playing sound");
                    // Dynamically add - there were issues where audio played non-stop when I added AudioSource
                    // component to each knife blade. Also had "Ran out of Virtual Channel error.
                    audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.PlayOneShot(knifeSound);
                }
               
            }
            else
            {
                // Debug.Log("KnifeTrigger::OnTriggerEnter: knifeSound was null - no audio played.");
            }
            Debug.Log("KnifeTrigger::OnTriggerEnter: Struck by knife on killer wall. gg no re.");
            Debug.Log("GAME OVER KNIFE");
            gameOverScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
  