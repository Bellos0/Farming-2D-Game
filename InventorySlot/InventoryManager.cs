using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public InvenSlot[]? slotList;
    [SerializeField] public SeedSlot[]? SeedSlotList;
    [SerializeField] protected Item[] item;
    public GameObject itemPrefab;
    public static InventoryManager Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = this;

        // AddItem(0, slotList[4]);

    }
    public void AddItem(int id, InvenSlot? slot)
    {
        if (slot == null)
            return;
        if (slot.CurItem == null)
        {
            GameObject newItem = Instantiate(itemPrefab, slot.visual_trans);
            InvenItem newItemData = newItem.GetComponent<InvenItem>();
            newItemData.Item = item[id];
            newItemData.Qty = 1;
            newItemData.OriginalTrans = slot.visual_trans;
            newItemData.transform.SetParent(slot.visual_trans);
            newItemData.transform.localPosition = Vector3.zero;
            newItemData.Image.sprite = item[id].sprite;
            slot.CurItem = newItemData;
            return;
        }
        if (slot.CurItem != null)
        {
            if (slot.CurItem.Item == item[id])
            {
                slot.CurItem.Qty++;
                slot.CurItem.refreshQty();
                return;
            }
        }
    }


    public void AddItem(int id, SeedSlot? seedSlot)
    {
        if (seedSlot == null)
        {
            return;
        }

        if (seedSlot.CurItem == null)
        {
            GameObject newItem = Instantiate(itemPrefab, seedSlot.visual_trans);
            InvenItem newItemData = newItem.GetComponent<InvenItem>();
            newItemData.Item = item[id];
            newItemData.Qty = 1;
            newItemData.OriginalTrans = seedSlot.visual_trans;
            newItemData.transform.SetParent(seedSlot.visual_trans);
            newItemData.transform.localPosition = Vector3.zero;
            newItemData.Image.sprite = item[id].sprite;
            seedSlot.CurItem = newItemData;
            return;
        }
        if (seedSlot.CurItem != null)
        {
            if (seedSlot.CurItem.Item == item[id])
            {
                seedSlot.CurItem.Qty++;
                seedSlot.CurItem.refreshQty();
                return;
            }
        }
    }


    public void RemoveItem(InvenItem item)
    {
        Destroy(item.gameObject);
        InvenSlot slot = item.OriginalTrans.GetComponentInParent<InvenSlot>();
        slot.CurItem = null;
    }

}
