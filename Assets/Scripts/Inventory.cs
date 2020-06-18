﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int amountHotbarSlots;
    public List<GameObject> hotbarSlots;
    public GameObject slotGameObject;
    public SlotHolder hotbarSlotArea;
    public SlotHolder inventorySlotArea;
    public Hud hud;
    public GameObject equipmentManager;
    public KeyCode dropKey = KeyCode.G;
    public KeyCode interactObjectButton = KeyCode.Mouse0;
    public KeyCode inventoryKey = KeyCode.E;
    public KeyCode dropBackpackKey = KeyCode.Q;

    private GameObject backpackObject;

    private List<KeyCode> hotbarcodes;
    private int hotbarKey = 1;
    private GameObject EquippedItem = null;
    private GameObject currentSlot;


    private void Start()
    {
        hotbarSlots = new List<GameObject>();
        InitializeHotbar();
        for (int i = 0; i < amountHotbarSlots; i++)
        {
            GameObject tempSlot = Instantiate(slotGameObject);
            tempSlot.transform.SetParent(hotbarSlotArea.transform);
            hotbarSlots.Add(tempSlot);
        }
    }

    private void Update()
    {
        CheckInput();
    }

    public void PickupItem(GameObject item)
    {
        if (CheckIfItemEquipped() && EquippedItem.GetComponent<Item>().itemType != ItemTypes.Tool)
        {
            Debug.Log("not tool");
            return;
        }
        else if (CheckIfItemEquipped() && EquippedItem.GetComponent<Item>().itemType == ItemTypes.Tool)
        {
            Debug.Log("is tool");
            UnEquipItem();
        }
        PutItemInHotbar(item);
    }

    private void PutItemInHotbar(GameObject item)
    {
        Item currentItem = item.GetComponent<Item>();
        foreach (var slot in hotbarSlots)
        {
            Slot slotToCheck = slot.GetComponent<Slot>();
            if (slotToCheck.item == null)
            {
                //kan kleiner
                slotToCheck.item = currentItem;
                slotToCheck.ShowItemInSlot();
                //

                item.transform.SetParent(slotToCheck.transform);
                currentSlot = slot;
                currentItem.OnPickup();
                EquipItem(item);
                return;
            }
        }
    }

    private void EquipItem(GameObject item)
    {
        foreach (var equipmentitem in equipmentManager.transform.GetComponentsInChildren<Item>(true))
        {
            if (equipmentitem.itemID == item.GetComponent<Item>().itemID)
            {
                EquippedItem = equipmentitem.gameObject;
                equipmentitem.gameObject.SetActive(true);
                Destroy(EquippedItem.GetComponent<Rigidbody>());
            }
        }
    }

    private void CheckHotbarInput()
    {
        if (!CheckIfItemEquipped())
        {
            foreach (var key in hotbarcodes)
            {
                if (Input.GetKey(key))
                {
                    hotbarKey = Convert.ToInt32(key.ToString().Substring(5));
                    if (GetItemByHotbarId(hotbarKey) != null)
                    {
                        //EquipItem(hotbarKey - 1);
                    }
                    return;
                }
            }
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(dropBackpackKey))
        {
            RemoveBackPack();
        }
        else if (Input.GetKeyDown(dropKey))
        {
            CheckDrop();
        }
        else if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }
        CheckHotbarInput();
    }

    private void CheckDrop()
    {
        if (CheckIfItemEquipped())
        {
            Instantiate(currentSlot.GetComponent<Slot>().item.gameObject, EquippedItem.transform.position, EquippedItem.transform.rotation).SetActive(true);
            EquippedItem.SetActive(false);
            Destroy(currentSlot.GetComponent<Slot>().item.gameObject);
        }
    }
    private bool CheckIfItemEquipped()
    {
        if (EquippedItem != null)
        {
            return EquippedItem.activeSelf;
        }
        else return false;
    }
    private Item GetItemByHotbarId(int hotbarId)
    {
        Item itemToCheck = hotbarSlots[hotbarId - 1].GetComponent<Slot>().item;
        if (itemToCheck != null)
        {
            return itemToCheck;
        }
        else return null;
    }
    private void UnEquipItem()
    {
        EquippedItem.SetActive(false);
        hotbarKey = 0;
    }
    private void InteractWithObject()
    {
        if (Input.GetKey(interactObjectButton) && EquippedItem != null)
        {
            switch (EquippedItem.GetComponent<Item>().itemType)
            {
                case ItemTypes.Tool:
                    break;
                case ItemTypes.Weapon:
                    break;
                case ItemTypes.Food:
                    break;
                case ItemTypes.Object:
                    break;
                default:
                    break;
            }
        }
    }
    private void InitializeHotbar()
    {
        hotbarcodes = new List<KeyCode>
        {
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8
        };
    }

    public void AddBackpack(GameObject collectedBagpack)
    {
        if (backpackObject != null)
        {
            RemoveBackPack();
        }
        backpackObject = collectedBagpack;
        collectedBagpack.SetActive(false);

        Backpack backpack = backpackObject.GetComponent<Backpack>();

        foreach (var slot in backpack.slots)
        {
            slot.transform.SetParent(inventorySlotArea.transform);
        }
    }

    public void RemoveBackPack()
    {
        Backpack backpack = backpackObject.GetComponent<Backpack>();

        foreach (var slot in backpack.slots)
        {
            slot.transform.SetParent(backpack.transform);
        }
        backpack.gameObject.SetActive(true);
    }

    private void ToggleInventory()
    {
        Debug.Log("Inventory key pressed");
        if (inventorySlotArea.gameObject.activeSelf)
        {
            inventorySlotArea.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!inventorySlotArea.gameObject.activeSelf)
        {
            inventorySlotArea.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void SwitchItems()
    {
        if(inventorySlotArea.gameObject.activeSelf)
        {
            List<Slot> selectedSlots = new List<Slot>();
            foreach (var slot in hotbarSlots)
            {
                selectedSlots.Add(slot.GetComponent<Slot>());
            }
            foreach (var slot in backpackObject.GetComponent<Backpack>().slots)
            {
                selectedSlots.Add(slot.GetComponent<Slot>());
            }
        }
    }
}