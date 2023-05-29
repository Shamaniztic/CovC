using UnityEngine;
using System.Collections;

public class ActionPanelController : MonoBehaviour
{
    // Reference to the canvas group component
    private CanvasGroup canvasGroup;

    // Reference to the RectTransform of the panel
    private RectTransform rectTransform;

    // Reference to the camera
    public Camera mainCamera;

    // Fade duration
    private float fadeDuration = 0.5f;

    private void Awake()
    {
        // Get the canvas group and rect transform components
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Call this method to show the action panel
    public void ShowPanel(Vector3 unitPosition)
    {
        // Convert the unit's world position to screen position
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(unitPosition);

        // Position the panel to the right of the unit
        rectTransform.position = screenPosition + Vector2.right * (rectTransform.rect.width / 14);

        // Start the fade in coroutine
        StartCoroutine(FadeIn());
    }

    // Call this method to hide the action panel
    public void HidePanel()
    {
        // Start the fade out coroutine
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        float startTime = Time.time;
        while (Time.time < startTime + fadeDuration)
        {
            canvasGroup.alpha = (Time.time - startTime) / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private IEnumerator FadeOut()
    {
        float startTime = Time.time;
        while (Time.time > startTime + fadeDuration)
        {
            canvasGroup.alpha = 1 - ((Time.time - startTime) / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
