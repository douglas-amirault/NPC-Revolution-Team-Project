using UnityEngine;



public class ProximityActivator : MonoBehaviour

{

    void OnTriggerEnter(Collider other)

    {
        //Debug.Log("Was triggered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Goal");
        }

    }
}