using System.Collections;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    private Animator animator;
    private float timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        timer = 5f; // Set initial timer to 5 seconds
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PlayRandomAnimation();
            timer = 5f; // Reset timer to 5 seconds
        }
    }

    private void PlayRandomAnimation()
    {
        int randomAnimation = Random.Range(0, 4);

        switch (randomAnimation)
        {
            case 0:
                animator.Play("ForwardIdle");
                break;
            case 1:
                animator.Play("BackIdle");
                break;
            case 2:
                animator.Play("SideIdle");
                break;
            case 3:
                animator.Play("SideIdle");
                animator.transform.localScale = new Vector3(-1, 1, 1); // Flip character for other side
                break;
        }
    }
}