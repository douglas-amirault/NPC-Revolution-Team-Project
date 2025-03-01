using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{

    public float speed = 5.0f;

    private Rigidbody rigidBody;

    // for jumping
    private bool isGrounded;
    public float jumpStrength = 10f;

    void Start()
    {

        rigidBody = GetComponent<Rigidbody>();
        isGrounded = true;

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

        // trying to get rotate working
        //rigidBody.MoveRotation(rigidBody.rotation * Quaternion.AngleAxis(-inputVelocity.x * Time.deltaTime * 1f, Vector3.up));

    }
    void Update()
    {
        // jump
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            if (isGrounded)
            {
                Debug.Log("Jump");
                rigidBody.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
                isGrounded = false;
            }
        }
    }




    private void OnCollisionEnter(Collision c)
    {
        // jump completed
        if (c.transform.gameObject.tag == "Ground")
        {
            Debug.Log("Land");
            isGrounded = true;
        }

        // game over on bad touch obstacles
        if (c.transform.gameObject.tag == "Obstacle")
        {
            Time.timeScale = 0f;
            Debug.Log("Game Over");
        }

        // shut off roomba instead of 

        if (c.gameObject.CompareTag("Enemy"))
        {
            // check it's not powered off
            Roomba enemyScript = c.gameObject.GetComponent<Roomba>();
            if (enemyScript.IsPowerOn())
            {
                Time.timeScale = 0f;
                Debug.Log("Game Over");
            }


        }
        //c.transform.gameObject.name
    }

    private void OnTriggerEnter(Collider c)
    {
        // press button: roomba turn off
        if (c.transform.gameObject.name == "PowerButton")
        {
            Roomba enemyScript = c.gameObject.GetComponentInParent<Roomba>();
            
            if(enemyScript.IsPowerOn())
            {
                enemyScript.TurnRoombaOff();

                // add a bounce to get that character from the roomba to see the pretty animation
                rigidBody.AddForce(Vector3.up * jumpStrength * 0.75f, ForceMode.Impulse);

                Debug.Log("Roomba should be off now");

            }
        }

        else if (c.transform.gameObject.name == "Roomba")
        {
            Roomba enemyScript = c.gameObject.GetComponent<Roomba>();
            if(enemyScript.IsPowerOn())
            {
                enemyScript.Warning();
            }
        }

        else if (c.transform.gameObject.name == "Body")
        {
            Roomba enemyScript = c.gameObject.GetComponentInParent<Roomba>();
            if (enemyScript.IsPowerOn())
            {
                enemyScript.ChasePlayer();
            }
        }

    }

    private void OnTriggerExit(Collider c)
    {
        if (c.transform.gameObject.name == "Roomba")
        {
            Roomba enemyScript = c.gameObject.GetComponent<Roomba>();
            if (enemyScript.IsPowerOn())
            {
                enemyScript.OffWarning();
            }
        }

        else if (c.transform.gameObject.name == "Body")
        {
            Roomba enemyScript = c.gameObject.GetComponentInParent<Roomba>();
            if (enemyScript.IsPowerOn())
            {
                enemyScript.OffChasePlayer();
            }
        }
    }
}
