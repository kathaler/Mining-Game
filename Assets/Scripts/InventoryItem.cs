using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    public Item data {get; private set;}
    public int stackSize {get; private set;}

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
}
