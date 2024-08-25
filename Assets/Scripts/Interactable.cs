using UnityEngine;
public class Interactable : MonoBehaviour
{
    [SerializeField] string itemName;

    public void PickedUp()
    {
        Inventory.Instance.PutInInventory(itemName);

        gameObject.SetActive(false);
    }
}