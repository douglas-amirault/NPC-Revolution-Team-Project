using UnityEngine;

public class RunState : MovementStateBase
{
    public override void StartState(MovementStateController movement)
    {
        movement.animator.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateController movement)
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) ExitState(movement, movement.IdleState);
        if (Input.GetKey(KeyCode.Space))
        {
            movement.animator.SetBool("Jump", true);
        }
        else
        {
            movement.animator.SetBool("Jump", false);
            movement.ChangeState(movement.RunState);
        }
    }

    void ExitState(MovementStateController movement, MovementStateBase state)
    {
        movement.animator.SetBool("Running", false);
        movement.ChangeState(state);
    }
}
