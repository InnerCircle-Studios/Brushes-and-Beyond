using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;


public class PlayerCombatMusic : MonoBehaviour
{
    public float checkRadius = 5.0f; // The radius within which we check for enemies
    public LayerMask enemyLayer; // Set this to the enemy layer in the inspector
    public float fadeDuration = 1.0f; // How long the music takes to fade in/out

    private bool isInCombat = false; // Current combat state
    private bool wasInCombat = false; // Previous combat state
    public AudioMixer audiomixer;

    private void Update()
    {
        CheckForEnemies();
    }

    private void CheckForEnemies()
    {
        // Check if any enemies are within the checkRadius of the player
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, checkRadius, enemyLayer);

        isInCombat = enemies.Length > 0;

        // Check if the state has changed
        if (isInCombat != wasInCombat)
        {
            // If we entered combat
            if (isInCombat)
            {
                AudioManager.instance.ToggleMusic(true);
            }
            // If we exited combat
            else
            {
                AudioManager.instance.ToggleMusic(false);
            }
        }

        wasInCombat = isInCombat;
    }
}