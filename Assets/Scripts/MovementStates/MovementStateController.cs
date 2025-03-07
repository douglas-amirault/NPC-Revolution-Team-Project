using UnityEngine;

public class MovementStateController : MonoBehaviour
{
    // States
    public MovementStateBase thisState;
    public MovementStateBase nextState;
    public IdleState IdleState = new IdleState();
    public RunState RunState = new RunState();
    public JumpState JumpState = new JumpState();

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
    }

    // Update is called once per frame
    void Update()
    {
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

    public bool IsGrounded()
    {
        spherePosition = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        if (Physics.CheckSphere(spherePosition, controllerVar.radius - 0.05f)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            velocityValue.y += gravityValue * Time.deltaTime;
        }
        else if (velocityValue.y < 0)
        {
            velocityValue.y = -2;
        }

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
}
 