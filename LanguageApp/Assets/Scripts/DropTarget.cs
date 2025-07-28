using UnityEngine;
using UnityEngine.EventSystems;

public class DropTarget : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggedItem = eventData.pointerDrag?.GetComponent<DraggableItem>();

        if (draggedItem != null)
        {
            draggedItem.wasDroppedOnTarget = true;  // Mark as successful
            GameManager.Instance.CheckSelection(draggedItem);
        }
    }    

}

