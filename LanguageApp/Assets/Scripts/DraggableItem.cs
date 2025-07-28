using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemName; // "apple", "ball", etc.
    public bool wasDroppedOnTarget = false;


    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector3 originalPosition;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    originalPosition = rectTransform.position;
    //    canvasGroup.blocksRaycasts = false;
    //}

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!wasDroppedOnTarget)
        {
            // Snap back if not dropped on avatar
            //rectTransform.anchoredPosition = originalPosition;
            StartCoroutine(SmoothSnapBack());
        }

        wasDroppedOnTarget = false; // reset for next time
    }

    public void SnapBackToStart()
    {
        StartCoroutine(SmoothSnapBack());
    }

    private IEnumerator SmoothSnapBack()
    {
        float duration = 0.25f;
        float elapsed = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 endPos = originalPosition;

        while (elapsed < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
    }



}
