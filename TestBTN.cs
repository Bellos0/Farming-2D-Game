using UnityEngine;

public class TestBTN : MonoBehaviour
{
    int count = 0;

    public void AddClick()
    {
        InventoryManager.Instance.AddItem(0, InventoryManager.Instance.slotList[count]);
        //count++;
    }
}
