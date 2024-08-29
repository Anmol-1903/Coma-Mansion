using UnityEngine;
public class Interactable : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] Sprite sprite;

    public string GetName() {  return itemName; }
    public Sprite GetSprite() { return sprite; }
    public void PickedUp()
    {
        Inventory.Instance.PutInInventory(gameObject);

        gameObject.SetActive(false);
    }
}