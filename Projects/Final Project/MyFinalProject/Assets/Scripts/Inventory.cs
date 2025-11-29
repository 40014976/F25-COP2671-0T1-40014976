using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryEntry> items = new List<InventoryEntry>();

    public void AddItem(HarvestableItem newItem)
    {
        var existing = items.Find(i => i.item == newItem);

        if (existing != null)
        {
            existing.quantity++;
        }
        else
        {
            items.Add(new InventoryEntry { item = newItem, quantity = 1 });
        }
    }
}
