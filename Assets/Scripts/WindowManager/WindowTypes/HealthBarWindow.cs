using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class HealthBarWindow {
    public string Name;
    public Image Element; // UI element for the health bar
    public Sprite[] HealthSprites; // Array of sprites for different health states
}