using UnityEngine;
using UnityEngine.EventSystems;

public class EQ_Slot : InvenSlot, IDropHandler
{
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InvenItem dragItem = eventData.pointerDrag.GetComponent<InvenItem>();
            if (dragItem.OriginalTrans != null)
            {
                InvenSlot sourceSlot = dragItem.OriginalTrans.GetComponentInParent<InvenSlot>();
                if (sourceSlot != null && sourceSlot != this)
                {
                    sourceSlot.CurItem = null;
                }
            }
            if (CurItem == null && dragItem != null && ItemInSlotType == dragItem.Item.Type && dragItem.Item.isWearable)
            {
                CurItem = dragItem;
                CurItem.OriginalTrans = visual_trans;
                CurItem.transform.SetParent(visual_trans);
                CurItem.transform.localPosition = Vector3.zero;
            }
        }
    }
}
