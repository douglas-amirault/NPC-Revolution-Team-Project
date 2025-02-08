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

        // still need y component of velocity so gravity cna take effect
        Vector3 inputVelocity = verticalVelocity + horizontalVelocity;
        rigidBody.velocity = new Vector3(inputVelocity.x, rigidBody.velocity.y, inputVelocity.z);


    }
}