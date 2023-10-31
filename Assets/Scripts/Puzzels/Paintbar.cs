using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
public class Paintbar : MonoBehaviour {
    [SerializeField]
    private int maxPaints = 3; // Maximum number of paints player can hold
    [SerializeField]
    private UnityEvent onMaxPaintsCollected; // Event to trigger when player collects 3 paints
    [SerializeField]
    private UnityEvent onPaintsUsed;
    [SerializeField]
    private TextMeshProUGUI paintCounterUI; // UI text to show number of paints collected
    [SerializeField]
    private Image paintImagePrefab; // The UI Image prefab for the paint
    [SerializeField]
    private Transform paintContainer; // The parent container for the paint images

    [SerializeField] private int currentPaintCount = 0; // Current count of paints
    private bool _isPaintPressed = false;

    public bool HasMaxPaints { get; private set; } = false; // Variable that checks if player has 3 paints

    private List<Image> displayedPaintImages = new List<Image>(); // List to hold the displayed paint images

    private void Awake() {
        // Initialize UI
        UpdatePaintUI();
    }
    // Method to be called when gameItem event is triggered
    public void OnGameItemTriggered() {
        CollectPaint();
    }
    private void CollectPaint() {
        if (currentPaintCount < maxPaints) {
            currentPaintCount++; // Increase paint count
            UpdatePaintUI(); // Update the UI
            // Check if player has collected 3 paints
            if (currentPaintCount == maxPaints) {
                HasMaxPaints = true; // Set the variable to true
            }
        }
    }

    public void InteractOnBlockade() {
        _isPaintPressed = true;
    }

    public void Update() {
        UsePaints();
    }

    // Method to use the paints
    public void UsePaints() {
        if (HasMaxPaints && _isPaintPressed) {
            currentPaintCount = 0; // Reset paint count
            HasMaxPaints = false; // Reset the variable
            UpdatePaintUI(); // Update the UI
            onPaintsUsed.Invoke(); // Trigger the event
            _isPaintPressed = false;
        }
    }

    // Helper method to update the UI text
    private void UpdatePaintUI() {
        // Clear existing paint images
        foreach (Image img in displayedPaintImages) {
            Destroy(img.gameObject);
        }
        displayedPaintImages.Clear();
        Debug.Log($"Cleared existing paints. Current paint count: {currentPaintCount}");
        // Display current paint count as images
        for (int i = 0; i < currentPaintCount; i++) {
            Image newImage = Instantiate(paintImagePrefab, paintContainer);
            if (newImage != null) {
                Debug.Log($"Instantiated paint image {i + 1}");
                displayedPaintImages.Add(newImage);
            }
            else {
                Debug.Log("Failed to instantiate paint image");
            }
        }
    }
}