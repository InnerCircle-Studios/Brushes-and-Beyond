using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    void Start()
    {
        // Get the Animator attached to the button
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover animation
        animator.Play("Button1Animation");
        animator.Play("Button1Idle");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Play exit animation (if you have one)
        animator.Play("Button1Idle");
    }
}