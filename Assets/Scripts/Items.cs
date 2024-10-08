using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "ItemList/Items")]
public class Items : ScriptableObject
{
    [SerializedDictionary("Item Name", "Item Attributes")]
    public SerializedDictionary<string, Item> items;
}

[System.Serializable]
public class Item
{
    public Sprite itemUI;
}