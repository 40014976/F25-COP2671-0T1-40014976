using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform slotParent;
    public GameObject slotPrefab;

    public void RefreshUI()
    {
        foreach (Transform child in slotParent)
            Destroy(child.gameObject);

        foreach (var entry in inventory.items)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slot.transform.Find("Icon").GetComponent<Image>().sprite = entry.item.icon;
            slot.transform.Find("Quantity").GetComponent<TMP_Text>().text = entry.quantity.ToString();
        }
    }
}
