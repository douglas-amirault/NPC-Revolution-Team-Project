public class JumpState : MovementStateBase
{
    public override void StartState(MovementStateController movement)
    {
        movement.animator.SetTrigger("Jump");
    }

    public override void UpdateState(MovementStateController movement)
    {
        movement.ChangeState(movement.RunState);
    }
}
