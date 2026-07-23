using UnityEngine;

public class PickupStuff : MonoBehaviour
{
    public ItemModels itemdata;

    public ItemModels Initialize(ItemModels item)
    {
        itemdata = item;
        gameObject.GetComponent<SpriteRenderer>().sprite = itemdata.sprite;
        return itemdata;
    }

}
