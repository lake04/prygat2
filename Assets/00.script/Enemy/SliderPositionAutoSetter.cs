using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector2 distance = Vector2.down * 20.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;
    private Camera mainCamera;

    public void Setup(Transform target)
    {
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform is missing on this GameObject.");
            return;
        }

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not found or not tagged as 'MainCamera'.");
        }
    }

    private void LateUpdate()
    {
        if (targetTransform == null || rectTransform == null || mainCamera == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPosition = mainCamera.WorldToScreenPoint(targetTransform.position);
        Canvas canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            Vector2 canvasPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                screenPosition, canvas.worldCamera, out canvasPosition);
            rectTransform.localPosition = canvasPosition + distance;
        }
        else
        {
            Debug.LogError("No Canvas found in parent hierarchy.");
        }
    }
}
