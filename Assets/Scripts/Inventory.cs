using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] Items ItemList;

    [SerializeField] Transform inventoryTransform;
    [SerializeField] GameObject inventoryPrefab;
    [SerializeField] int inventorySize = 5;

    [SerializeField] List<Image> ItemUIs = new List<Image>();

    public string[] dictionaryKeys;
    public int[] dictionaryValues;


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

        InitializeDictionary();

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
    public bool CanPickup { get { return canPickup; } }
    public void PutInInventory(GameObject pickedItem)
    {
        Interactable interactable = pickedItem.GetComponentInChildren<Interactable>();
        if (!canPickup)
        {
            Debug.Log("Inventory Limit Reach");
            //Can't Pickup
            return;
        }
        else
        {
            ItemUIs[inventoryIndex].enabled = true;
            ItemUIs[inventoryIndex].name = interactable.GetName();

            if (ItemList.items.TryGetValue(interactable.GetName(), out var item))
            {
                ItemUIs[inventoryIndex].sprite = interactable.GetSprite();
            }
            inventoryIndex++;

            dictionaryValues[GetIndex(interactable.GetName())]++;

            CheckGroupOf3();
        }
    }

    void CheckGroupOf3()
    {
        for (int i = 0; i < dictionaryValues.Length; i++)
        {
            if (dictionaryValues[i] >= 3)
            {
                dictionaryValues[i] = 0;
                for (int j = inventorySize - 1; j >= 0; j--)
                {
                    if (ItemUIs[j].name == dictionaryKeys[i])
                    {
                        Destroy(ItemUIs[j].transform.parent.gameObject);
                        ItemUIs.RemoveAt(j);
                        
                    }
                }
                inventoryIndex -= 3;
                for (int x = 2; x < 5; x++)
                {
                    GameObject temp = Instantiate(inventoryPrefab, inventoryTransform);
                    ItemUIs.Add(temp.GetComponentInChildren<Image>());
                    ItemUIs[x].enabled = false;

                }
            }
        }
    }

    void InitializeDictionary()
    {
        dictionaryKeys = ItemList.items.Keys.ToArray();

        dictionaryValues = new int[dictionaryKeys.Length];

        for(int i = 0;i < dictionaryKeys.Length;i++)
        {
            dictionaryValues[i] = 0;
        }
    }

    public int GetIndex(string itemName)
    {
        for(int i = 0; i < dictionaryKeys.Length;i++)
        {
            if (dictionaryKeys[i] == itemName)
            {
                return i;
            }
        }
        return 0;
    }
}