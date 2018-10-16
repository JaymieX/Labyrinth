public class JumpState : InAirState
{
    public override void Begin()
    {
        PlayerManager.Instance.PlayerController.MoveDirection.y = 5.0f;
        base.Begin();
    }
}
