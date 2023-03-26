using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySystem : MonoBehaviour
{
    public event EventHandler onInventoryChangedEvent;

    private Dictionary<Item, InventoryItem> items;
    public List<InventoryItem> inventory;

    public static InventorySystem instance {get; private set;}

    void Awake()
    {
        if(instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
            inventory = new List<InventoryItem>();
            items = new Dictionary<Item, InventoryItem>();
        }

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
        onInventoryChangedEvent?.Invoke(this, EventArgs.Empty);

    }

    public void PrintInventory() {
        foreach (var InventoryItem in inventory)
        {
            print(InventoryItem.getDataString());
        }
    }
}
