using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInventoryItem
{
    int ItemID { get; set; }
    int Size { get; set; }
    string Name { get; set; }
    string Type { get; set; }
    string ItemDescription { get; set; }
    Sprite Icon { get; set; }
    bool Equipped { get; set; }

    void OnPickup();
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}