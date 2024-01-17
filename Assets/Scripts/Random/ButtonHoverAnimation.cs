using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // Required for accessing TextMeshPro components

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    private Vector3 originalPosition;

    void Start()
    {
        // Get the RectTransform component attached to the button
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.localPosition;

        // Get the TextMeshProUGUI component from the button's children (if it exists)
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Move the button a bit down
        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y - 15, rectTransform.localPosition.z);

        // Make the text bold if the TextMeshProUGUI component exists
        if (textMeshPro != null)
            textMeshPro.fontStyle = FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Move the button back to its original position
        rectTransform.localPosition = originalPosition;

        // Revert the text style to normal if the TextMeshProUGUI component exists
        if (textMeshPro != null)
            textMeshPro.fontStyle = FontStyles.Normal;
    }
}
