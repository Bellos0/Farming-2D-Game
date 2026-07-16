using UnityEngine;
public class SeedInvenManager : InventoryManager
{
    public static SeedInvenManager Instance;
    int selectSeed;

    public int SelectSeed { get => selectSeed; set => selectSeed = value; }

    public
      void Awake()
    {

        Instance = this;
        SelectSeed = -1;

        for (int i = 0; i < item.Length; i++)
        {
            AddItem(i, SeedSlotList[i]);
        }
    }

    public Item? PickUpSeed(int select)
    {
        Debug.Log("pick up seed ");
        if (select < 0)
        {
            return null;
        }
        if (SeedSlotList[select]?.CurItem?.Item.Type == Item.ItemType.seed)
        {
            Item seedItem = SeedSlotList[select].CurItem.Item;
            Debug.Log("return pickup seed");
            return seedItem;
        }
        else
        {
            Debug.Log("seeditem is null");
            return null;
        }

    }

}
