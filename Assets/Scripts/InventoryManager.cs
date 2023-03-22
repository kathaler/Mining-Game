using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    public GameObject m_slotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InventorySystem.instance.onInventoryChangedEvent += OnUpdateInventory;
    }

    private void OnUpdateInventory(object sender, EventArgs e) {
        foreach(Transform t in transform) {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }

    public void DrawInventory() {
        foreach(InventoryItem item in InventorySystem.instance.inventory) {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item) {
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(transform, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }
}
