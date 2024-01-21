using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Sprite[] healthSprites; // Array of health bar sprites
    private Image healthBarImage;

    void Start()
    {
        healthBarImage = GetComponent<Image>();
    }

    public void UpdateHealthBar(int currentHP)
    {
        // Assuming 10 is max HP and sprites are ordered from full to empty
        healthBarImage.sprite = healthSprites[currentHP];
    }
}