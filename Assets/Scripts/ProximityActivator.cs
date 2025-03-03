using UnityEngine;



public class ProximityActivator : MonoBehaviour

{

    void OnTriggerEnter(Collider other)

    {
        Debug.Log("Was triggered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            Debug.Log("Player Goal");
        }

    }
}