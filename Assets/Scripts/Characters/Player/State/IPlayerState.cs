public interface IPlayerState
{
    void Begin();
    void End();

    void HandleInput();

    IPlayerState UpdateState();
}