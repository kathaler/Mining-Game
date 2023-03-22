using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    public Item data;
    public int stackSize;

    public InventoryItem(Item source) {
        data = source;
        AddToStack();
    }

    public void AddToStack() {
        stackSize++;
    }

    public void RemoveFromStack() {
        if(stackSize > 0) {
            stackSize--;
        }
    }

    public string getDataString() {
        return data.type + " : " + stackSize;

    }

}
