using UnityEngine;



public class ProximityActivator : MonoBehaviour

{

    void OnTriggerEnter(Collider other)

    {
        //Debug.Log("Was triggered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            // IN THEORY WHAT SHOULD HAPPEN: 
            // find and play roomba death animation
            // play goal reached animation 
            // animate player going to door leading to next level
            Time.timeScale = 0f;
            Debug.Log("Player Goal");
        }

    }
}