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
    private ItemUse itemUse;
    public int itemID;
    public string itemName;
    public ItemTypes itemType;
    public Sprite icon;
    public string itemDescription;
    public int itemSize;
    public bool equipped;
    public int hotbarID = -1;
    public Animator animator;

    [HideInInspector]
    public GameObject EquippedItem;
    [HideInInspector]
    public GameObject EquipmentManager;

    public void Start()
    {
        EquipmentManager = GameObject.FindWithTag("EquipManager");
    }

    public void Update()
    {
       
    }

    public void OnPickup()
    {
        equipped = true;
        gameObject.SetActive(false);
    }

    public void OnUse(GameObject Object)
    {
        itemUse = new ItemUse();
        if (itemType == ItemTypes.Tool)
        {
            itemUse.UseTool(this, Object);
        }
        if (itemType == ItemTypes.Food)
        {
            itemUse.UseFood(this);
        }
    }
}