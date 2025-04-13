using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MovementStateController : MonoBehaviour
{
    // States
    public MovementStateBase thisState;
    public MovementStateBase nextState;
    public IdleState IdleState = new IdleState();
    public RunState RunState = new RunState();
    public JumpState JumpState = new JumpState();
    public GameObject gameOverScreen;

    // Animator
    [HideInInspector] public Animator animator;

    // Movement
    public float movementSpeed = 3;
    [HideInInspector] public Vector3 direction;
    public float horizontalInput, verticalInput;

    CharacterController controllerVar;

    Vector3 spherePosition;

    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float jumpValue = 60;
    [SerializeField] float descentValue = -20;
    Vector3 velocityValue;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controllerVar = GetComponent<CharacterController>();
        ChangeState(IdleState);
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // need to add to avoid interfering with the UI
        // Prevent movement if UI is active
        //if (EventSystem.current.IsPointerOverGameObject()) return;

        GetDirectionAndMove();
        Gravity();

        animator.SetFloat("horizontalInput", horizontalInput);
        animator.SetFloat("verticalInput", verticalInput);

        thisState.UpdateState(this);
    }

    public void ChangeState(MovementStateBase state)
    {
        thisState = state;
        thisState.StartState(this);
    }

    void GetDirectionAndMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        direction = transform.forward * verticalInput + transform.right * horizontalInput;

        controllerVar.Move(direction * movementSpeed * Time.deltaTime);
    }

    /*public bool IsGrounded()
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        if (Physics.CheckSphere(spherePosition, controllerVar.radius - 0.05f)) return true;
        return false;
    }*/

    void Gravity()
    {
        if (/*!IsGrounded()*/true)
        {
            velocityValue.y += gravityValue * Time.deltaTime;
        }
        /*else if (velocityValue.y < 0)
        {
            velocityValue.y = -2;
        }*/

        controllerVar.Move(velocityValue * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        controllerVar = GetComponent<CharacterController>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition, controllerVar.radius - 0.05f);
    }

    public void JumpForce()
    {
        velocityValue.y = jumpValue;
        Debug.Log(velocityValue.y);
    }
    public void Descent()
    {
        velocityValue.y = descentValue;
        Debug.Log(velocityValue.y);
    }

    private void OnCollisionEnter(Collision c)
    {
        
        if (c.transform.gameObject.tag == "Knife")
        {
            //Time.timeScale = 0f;
            
            //gameOverScreen.SetActive(true);

        }

        // TODO: end game stuff here!!!!
        // game over on bad touch obstacles
        if (c.transform.gameObject.tag == "Obstacle")
        {
            Time.timeScale = 0f;
            Debug.Log("Game Over");
            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI
            Cursor.visible = true;

        }

        // shut off roomba instead of vice versa
        if (c.gameObject.CompareTag("Enemy"))
        {
            // check it's not powered off
            Roomba enemyScript = c.gameObject.GetComponent<Roomba>();
            if (enemyScript.IsPowerOn())
            {
                // also play gameOverSound
                enemyScript.PlayGameOverSoud();

                Time.timeScale = 0f;
                Debug.Log("Game Over");
                gameOverScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None; // Unlock cursor for UI
                Cursor.visible = true;
            }

        }
    }

    private void OnTriggerEnter(Collider c)
    {
        // press button: roomba turn off
        if (c.transform.gameObject.name == "PowerButton")
        {
            Roomba enemyScript = c.gameObject.GetComponentInParent<Roomba>();

            if (enemyScript.IsPowerOn())
            {
                enemyScript.TurnRoombaOff();
                Debug.Log("Roomba should be off now");

            }
        }

        else if (c.transform.gameObject.name == "Roomba")
        {
            Roomba enemyScript = c.gameObject.GetComponent<Roomba>();
            if (enemyScript.IsPowerOn())
            {
                enemyScript.Warning();
            }
        }

        else if (c.transform.gameObject.name == "Body")
        {
            Roomba enemyScript = c.gameObject.GetComponentInParent<Roomba>();
            if (enemyScript != null)
            {
                if (enemyScript.IsPowerOn())
                {
                    enemyScript.ChasePlayer();
                }
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
 