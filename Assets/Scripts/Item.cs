﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemTypes
{
    Tool,
    Weapon,
    Food,
    Object
}
public class Item : MonoBehaviour
{
    
    public int itemID;
    public string itemName;
    public ItemTypes itemType;
    public Sprite icon;
    public string itemDescription;
    public int itemSize;
    public bool equipped;
    public int hotbarID = -1;

    [HideInInspector]
    public GameObject EquippedItem;
    [HideInInspector]
    public GameObject EquipmentManager;

    private ItemUse itemUse;

    public void Start()
    {
        EquipmentManager = GameObject.FindWithTag("EquipManager");
    }

    public void Update()
    {
        if (equipped)
        {

        }
    }

    public void OnPickup()
    {
        equipped = true;
        gameObject.SetActive(false);
    }

    public void OnUse(GameObject Object)
    {
        if (this.itemType == ItemTypes.Tool)
        {
            itemUse.UseTool(this, Object);
        }
        if (this.itemType == ItemTypes.Food)
        {
            itemUse.UseFood(this);
        }
    }
}
