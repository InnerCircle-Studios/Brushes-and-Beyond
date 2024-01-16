using UnityEngine;
using System;

[Serializable]
public class HealthBarWindow : MonoBehaviour {
    private Animator animator; // Animator for the health bar

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void SetSprite(int index) {
        animator.Play($"Health {index}");
    }
}