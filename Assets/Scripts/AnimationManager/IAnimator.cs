public interface IAnimator {

    public void Play(string animationName, MovementDirection direction);
    public void Play(string animationName);
    public void PlayPrevious();
    public float GetAnimationDuration();
    public void Stop();

}