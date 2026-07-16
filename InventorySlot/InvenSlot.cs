using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// thuc hien logic tha item vao slot trong inventory
/// con tha nhu nao cac thu thi se kiem soat o inventory manager 
/// </summary>
public class InvenSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] InvenItem curItem;
    [SerializeField] public Transform? visual_trans;
    [SerializeField] public Item.ItemType ItemInSlotType;




    public enum SlotType
    {
        head, armor, pants, hanlde, wing, pet
    }

    public InvenItem CurItem { get => curItem; set => curItem = value; }

    private void Awake()
    {
        CurItem = GetComponentInChildren<InvenItem>() ?? null;

    }

    public virtual void OnDrop(PointerEventData eventData)
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

            if (CurItem == null && dragItem != null)
            {
                CurItem = eventData.pointerDrag.GetComponent<InvenItem>();
                CurItem.OriginalTrans = visual_trans;
                CurItem.transform.SetParent(visual_trans);
                CurItem.transform.localPosition = visual_trans.localPosition;
                return;
            }
            if (CurItem != null && dragItem != null && curItem.Item.id == dragItem.Item.id && curItem.Item.isStackable)
            {
                curItem.Qty = curItem.Qty + dragItem.Qty;
                curItem.refreshQty();
                InventoryManager.Instance.RemoveItem(dragItem);
                return;
            }
        }
    }
}
