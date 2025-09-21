using UnityEngine;

public class PickupResources : MonoBehaviour
{
    [field: SerializeField] public InventoryOLD Inventory { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ResourcePickup pickup = collision.gameObject.GetComponent<ResourcePickup>();

        if (pickup)
        {
            Inventory.AddResources(pickup.ResourceType, 1);
            Destroy(pickup.gameObject);
        }
    }
}
