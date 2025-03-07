using UnityEngine;

public class DropObject : MonoBehaviour
{
    public Rigidbody sphereRigidbody; // Assign the Sphere's Rigidbody
    public Transform roomba; // Assign Roomba
    public float dropDistance = 10f; // Distance to trigger drop

    private bool hasDropped = false;

    void Update()
    {
        if (roomba == null) return; // Exit if Roomba is missing

        float distance = Vector3.Distance(transform.position, roomba.position);
        //Debug.Log("Distance to Roomba: " + distance);

        if (!hasDropped && distance < dropDistance)
        {
           // Debug.Log("Roomba is close - Dropping sphere!");

            // Ensure the Rigidbody is set correctly
            sphereRigidbody.isKinematic = false; // Allow physics to move it
            sphereRigidbody.useGravity = true; // Ensure gravity is on

            hasDropped = true; // Prevent multiple drops
        }
    }
}
