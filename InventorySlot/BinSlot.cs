using UnityEngine;
using UnityEngine.EventSystems;

public class BinSlot : InvenSlot, IDropHandler
{
    public override void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop item into bin slot");
        InvenItem dragItem = eventData.pointerDrag.GetComponent<InvenItem>();
        CurItem = null;
        InventoryManager.Instance.RemoveItem(dragItem);
        return;
    }
}
