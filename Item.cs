using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Ingame Item")]
public class Item : ScriptableObject
{
    [Header("Game play only")]
    public ItemType Type;

    [Space]
    [Header("Item Info")]
    public int id;
    public string itemName;
    public int price;
    public string itemDescription;
    public Sprite sprite;
    public bool isStackable;
    public bool isWearable;
    public int minToGrow;

    public Item(string? name)
    {
        if (string.IsNullOrEmpty(name))
            return;
        else itemName = name;
    }

    public enum ItemType
    {
        head, armor, pants, hanlde, wing, pet, plant, animal, animalfood, fertilizer, poisonPlant, seed, bin, liem
    }
}
