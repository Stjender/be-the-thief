using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public GameObject Item;
    public int Id;
    public ItemTypes Type;
    public string Description;
    public Sprite Icon;
    public int HotbarId;

    public Slot ItemtoSlot(Item item)
    {
        if (item != null)
        {
            this.Item = item.gameObject;
        }
        this.Id = item.ItemID;
        this.Type = item.Type;
        this.Icon = item.Icon;
        this.Description = item.ItemDescription;
        this.HotbarId = item.HotbarID;
        return this;
    }

    public void ResetSlot()
    {
        this.Item = null;
        this.Id = 0;
        this.Type = 0;
        this.Icon = null;
        this.Description = null;
        this.HotbarId = 0;
    }
}
