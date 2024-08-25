using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] Items ItemList;

    [SerializeField] Transform inventoryTransform;
    [SerializeField] GameObject inventoryPrefab;
    [SerializeField] int inventorySize = 5;
    
    [SerializeField] List<Image> ItemUIs = new List<Image>();


    bool canPickup;
    int inventoryIndex = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        canPickup = true;
        ItemUIs.Clear();

        for (int i = 0; i < inventorySize; i++)
        {
            GameObject temp = Instantiate(inventoryPrefab, inventoryTransform);
            ItemUIs.Add(temp.GetComponentInChildren<Image>());
            ItemUIs[i].enabled = false;
        }
    }
    private void Update()
    {
        canPickup = (inventoryIndex >= inventorySize) ? false : true;
    }
    public bool CanPickup {  get { return canPickup; } }
    public void PutInInventory(string itemName)
    {
        if(!canPickup)
        {
            Debug.Log("Inventory Limit Reach");
            //Can't Pickup
            return;
        }
        else
        {
            ItemUIs[inventoryIndex].enabled = true;

            if (ItemList.items.TryGetValue(itemName, out var item))
            {
                ItemUIs[inventoryIndex].sprite = item.itemUI;
            }
            
            inventoryIndex++;
        }
    }
}