using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class KillerWallAudio : MonoBehaviour
{
    public AudioClip killerWallSound;
    private AudioSource audioSource;
    
    // audio clip sounds loud at normal volume
    public float soundVolume = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (killerWallSound != null)
            {
                // Debug.Log("KillerWallAudio::OnTriggerEnter: killerWallSound was NOT null");
                if (audioSource != null)
                {
                    // Debug.Log("KillerWallAudio::OnTriggerEnter: audioSource was NOT null");
                    audioSource.volume = soundVolume;
                    audioSource.PlayOneShot(killerWallSound);
                }
                else
                {
                    // Debug.Log("KillerWallAudio::OnTriggerEnter: audioSource was null - adding AudioSource component and playing sound");
                    // Dynamically add - there were issues where audio played non-stop when I added AudioSource for 
                    // component to each knife blade. Also had "Ran out of Virtual Channel error. (this was for C:\Users\derek\python3scripts\omscs\vgd_cs6457\milestones\NPC-Revolution-Team-Project\Assets\Scripts\KnifeTrigger.cs )                  
                    audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.volume = soundVolume;
                    audioSource.PlayOneShot(killerWallSound);
                }
               
            }
            else
            {
                // Debug.Log("KillerWallAudio::OnTriggerEnter: killerWallSound was null - no audio played.");
            }
        }
    }
}
