using UnityEngine;

public class IdleState : MovementStateBase
{
    public override void StartState(MovementStateController movement)
    { 
    }

    public override void UpdateState(MovementStateController movement)
    {
        if (movement.direction.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || 
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)  || 
                Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ||
                Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                movement.ChangeState(movement.RunState);
                Debug.Log("Key Pressed");
            }
            else
            {
                movement.ChangeState(movement.IdleState);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.nextState = this;
            movement.ChangeState(movement.JumpState);
        }
    }
}
