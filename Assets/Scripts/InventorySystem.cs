using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary<Item, InventoryItem> items;
    public List<InventoryItem> inventory;

    void Awake()
    {
        inventory = new List<InventoryItem>();
        items = new Dictionary<Item, InventoryItem>();
    }

    public void Add(Item referenceData) {
        if(items.TryGetValue(referenceData, out InventoryItem value)) {
            value.AddToStack();
        }
        else {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            items.Add(referenceData, newItem);
        }
    }
}
