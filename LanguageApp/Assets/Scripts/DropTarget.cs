using UnityEngine;
using UnityEngine.EventSystems;

public class DropTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggedItem = eventData.pointerDrag?.GetComponent<DraggableItem>();

        if (draggedItem != null)
        {
            Debug.Log("Dropped item: " + draggedItem.itemName);
            GameManager.Instance.CheckSelection(draggedItem.itemName);
        }
    }
}

