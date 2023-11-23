using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class AnimationManager : IAnimator {
    [SerializeField] private Animator animator;

    public AnimationManager(Animator animator) {
        this.animator = animator;
    }
    public void Play(string animationName, MovementDirection direction) {
        Debug.Log(ConvertMovementToAnimation(direction)+animationName);
        animator.Play(ConvertMovementToAnimation(direction)+animationName);
    }

    public void Play(string animationName) {
        animator.Play(animationName);

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