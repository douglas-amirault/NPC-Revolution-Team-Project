using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{

    public float speed = 5.0f;

    private Rigidbody rigidBody;

    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {

        /*        
                float vertical = Input.GetAxis("Vertical");

                float horizontal = Input.GetAxis("Horizontal");

                Vector3 movement = new Vector3(horizontal, 0, vertical) * speed;

                rigidBody.MovePosition(rigidBody.position + movement * Time.fixedDeltaTime);
        */

        float moveVertical = Input.GetAxis("Vertical");
        Vector3 verticalVelocity = transform.forward * speed * moveVertical;

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 horizontalVelocity = transform.right * speed * moveHorizontal;

        rigidBody.velocity = verticalVelocity + horizontalVelocity;
    }
}