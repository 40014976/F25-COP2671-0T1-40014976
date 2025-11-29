using UnityEngine;

public class Harvestable : MonoBehaviour
{
    public HarvestableItem itemData;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Inventory>().AddItem(itemData);
            Destroy(gameObject);
        }
    }
}
