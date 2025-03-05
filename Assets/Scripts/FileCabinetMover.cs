using UnityEngine;

public class FileCabinetMover : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float leftLimit = -4f;
    public float rightLimit = 4f;
    public Transform player; // Assign player in the Inspector
    public float stopDistance = 10f; // Distance at which the cabinet stops moving

    private bool movingRight = true;

    void Update()
    {
        // Check if player reference is missing
        if (player == null)
        {
           // Debug.LogError("Player reference is missing in FileCabinetMover!");
            return;
        }

        // Get the distance between the cabinet and player
        float distance = Vector3.Distance(transform.position, player.position);
        //Debug.Log("Distance to Player: " + distance); // Debug distance

        // Stop the cabinet if the player is within stopDistance
        if (distance <= stopDistance)
        {
            Debug.Log("Stopping Cabinet - Player is Close!");
            moveSpeed = Mathf.Lerp(moveSpeed, 0, Time.deltaTime * 3); // Smoothly slow down
            return;
        }
        else
        {
            moveSpeed = 3; // Reset speed when player is far
        }

        // Move the cabinet left and right
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }
}
