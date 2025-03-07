using UnityEngine;



public class ProximityActivator : MonoBehaviour

{

    public AudioClip doorLevelSound;
    private AudioSource audioSource;
    void OnTriggerEnter(Collider other)

    {
        Debug.Log("Was triggered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            // IN THEORY WHAT SHOULD HAPPEN: 
            // find and play roomba death animation
            // play goal reached animation 
            // animate player going to door leading to next level
            if (doorLevelSound != null)
            {
                Debug.Log("KillerWallAudio::OnTriggerEnter: doorLevelSound was NOT null");
                if (audioSource != null)
                {
                    Debug.Log("KillerWallAudio::OnTriggerEnter: audioSource was NOT null");
                    audioSource.PlayOneShot(doorLevelSound);
                }
                else
                {
                    Debug.Log("KillerWallAudio::OnTriggerEnter: audioSource was null - adding AudioSource component and playing sound");
                    // Dynamically add - there were issues where audio played non-stop when I added AudioSource for 
                    // component to each knife blade. Also had "Ran out of Virtual Channel error. (this was for C:\Users\derek\python3scripts\omscs\vgd_cs6457\milestones\NPC-Revolution-Team-Project\Assets\Scripts\KnifeTrigger.cs )                  
                    audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.PlayOneShot(doorLevelSound);
                }
            }
            else
            {
                Debug.Log("KillerWallAudio::OnTriggerEnter: doorLevelSound was null - no audio played.");
            }
            Time.timeScale = 0f;
            Debug.Log("Player Goal");
        }

    }
}