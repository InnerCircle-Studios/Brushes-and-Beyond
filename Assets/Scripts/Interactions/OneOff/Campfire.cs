using System.Collections;

using UnityEngine;

public class Campfire : MonoBehaviour {
    [SerializeField] private bool isLit = false;
    [SerializeField] private Sprite defaultSprite;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isOnCooldown = false;


    private void Start() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        animator.enabled = isLit;
        if (!isLit) {
            spriteRenderer.sprite = defaultSprite;
        }
    }

    public void ToggleFire() {
        if (!isOnCooldown) {
            isLit = !isLit;
            StartCoroutine(ToggleCooldown());
        }
    }

    IEnumerator ToggleCooldown() {
        isOnCooldown = true;
        yield return new WaitForSeconds(1f);
        isOnCooldown = false;
    }
}