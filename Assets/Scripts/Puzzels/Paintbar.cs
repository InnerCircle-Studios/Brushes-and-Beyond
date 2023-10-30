using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Paintbar : MonoBehaviour
{
    [SerializeField]
    private int maxPaints = 3; // Maximum number of paints player can hold

    [SerializeField]
    private UnityEvent onMaxPaintsCollected; // Event to trigger when player collects 3 paints
    [SerializeField]
    private UnityEvent onPaintsUsed;

    [SerializeField]
    private TextMeshProUGUI paintCounterUI; // UI text to show number of paints collected

    [SerializeField] private int currentPaintCount = 0; // Current count of paints

    public bool HasMaxPaints { get; private set; } = false; // Variable that checks if player has 3 paints

    private void Awake()
    {
        // Initialize UI
        UpdatePaintUI();
    }

    // Method to be called when gameItem event is triggered
    public void OnGameItemTriggered()
    {
        CollectPaint();
    }

    private void CollectPaint()
    {
        if (currentPaintCount < maxPaints)
        {
            currentPaintCount++; // Increase paint count
            UpdatePaintUI(); // Update the UI

            // Check if player has collected 3 paints
            if (currentPaintCount == maxPaints)
            {
                HasMaxPaints = true; // Set the variable to true
                onMaxPaintsCollected.Invoke(); // Trigger the event
            }
        }
    }

    // Method to use the paints
    public void UsePaints()
    {
        if (HasMaxPaints)
        {
            currentPaintCount = 0; // Reset paint count
            HasMaxPaints = false; // Reset the variable
            UpdatePaintUI(); // Update the UI
            onPaintsUsed.Invoke(); // Trigger the event
        }
    }

    // Helper method to update the UI text
    private void UpdatePaintUI()
    {
        if (paintCounterUI != null)
        {
            paintCounterUI.text = $"{currentPaintCount} / {maxPaints} Paints";
        }
    }
}