public interface IAnimator{
    
    public void Play(string animationName, MovementDirection direction);
    public void Play(string animationName);
    public void Stop();

}