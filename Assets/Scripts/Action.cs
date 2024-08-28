using UnityEngine;
public class Action : MonoBehaviour
{
    [SerializeField] Transform eyes;

    [SerializeField] float range = 5;

    private void Update()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(eyes.position, eyes.forward, out hit, range))
        {
            if (Input.GetButtonDown("Action"))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    Interactable item = hit.collider.GetComponent<Interactable>();
                    if(item != null)
                    {
                        if (Inventory.Instance.CanPickup)
                        {
                            item.PickedUp();
                        }
                    }
                }
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Debug.DrawRay(eyes.position, eyes.forward * range, Color.blue);
    }
#endif
}