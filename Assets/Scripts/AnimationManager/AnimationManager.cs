using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class AnimationManager : IAnimator {
    [SerializeField] private Animator animator;
    private string previousAnimation;

    public AnimationManager(Animator animator) {
        this.animator = animator;
    }
    public void Play(string animationName, MovementDirection direction) {
        string animation = ConvertMovementToAnimation(direction) + animationName;
        previousAnimation = animation;
        animator.Play(animation);
    }

    public void Play(string animationName) {
        previousAnimation = animationName;
        animator.Play(animationName);
    }

    public void PlayPrevious() {
        animator.Play(previousAnimation);
    }

    public float GetAnimationDuration() {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void Stop() {
        animator.StopPlayback();
    }

    private string ConvertMovementToAnimation(MovementDirection direction) {
        return direction switch {
            MovementDirection.UP => "Back",
            MovementDirection.DOWN => "Forward",
            MovementDirection.LEFT => "Side",
            MovementDirection.RIGHT => "Side",
            _ => "",
        };
    }
}