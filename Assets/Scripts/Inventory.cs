using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int hotbarSlots;
    public GameObject[] slots;
    public SlotHolder hotbarSlotArea;
    public SlotHolder inventorySlotArea;
    public Hud hud;
    public LoadHudSlots inventorySlots;
    public GameObject equipmentManager;
    public KeyCode dropKey = KeyCode.G;
    public KeyCode interactObjectButton = KeyCode.Mouse0;

    public GameObject backpackObject;

    private List<KeyCode> hotbarcodes;
    private int hotbarKey = 1;
    private GameObject EquippedItem = null;


    private void Start()
    {
        slots = new GameObject[hotbarSlots];
        InitializeHotbar();
        for (int i = 0; i < hotbarSlots; i++)
        {
            slots[i] = hotbarSlotArea.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        CheckHotbarInput();
        CheckDrop();
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
        for (int i = 0; i < hotbarSlots; i++)
        {
            if (slots[i].GetComponent<Slot>().item == null)
            {
                Slot openSlot = slots[i].GetComponent<Slot>();
                currentItem.hotbarID = i;

                openSlot.item = currentItem;
                openSlot.ShowItemInSlot();

                item.transform.SetParent(slots[i].transform);
                currentItem.OnPickup();
                Debug.Log(i);
                EquipItem(i);
                return;
            }
        }
    }
    private void EquipItem(int slotnumber)
    {
        if (slots[slotnumber].GetComponent<Slot>().item != null)
        {
            var itemName = slots[slotnumber].GetComponent<Slot>().item.itemName;
            if (slots[slotnumber].GetComponent<Slot>().item.itemName.Contains("(Clone)"))
            {
                var index = slots[slotnumber].GetComponent<Slot>().item.itemName.IndexOf("(");
                itemName = slots[slotnumber].GetComponent<Slot>().item.itemName.Substring(0, index);
            }
            Debug.Log(slots[slotnumber].GetComponent<Slot>().item.itemName);
            EquippedItem = equipmentManager.transform.Find(itemName).gameObject;
            Debug.Log(slotnumber);
            EquippedItem.GetComponent<Item>().hotbarID = slotnumber;
            Destroy(EquippedItem.gameObject.GetComponent<Rigidbody>());
            equipmentManager.transform.Find(EquippedItem.name).gameObject.SetActive(true);
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
                        EquipItem(hotbarKey - 1);
                    }
                    return;
                }
            }
        }
    }
    private void CheckDrop()
    {
        if (Input.GetKey(dropKey) && EquippedItem != null)
        {
            var hotbarId = EquippedItem.GetComponent<Item>().hotbarID + 1;
            Debug.Log(hotbarId);
            string panelString = "Panel";
            if (CheckIfItemEquipped())
            {
                panelString += " (" + hotbarId + ")";
            }
            else return;
            Debug.Log(panelString);
            foreach (Transform child in hotbarSlotArea.transform.Find(panelString))
            {
                if (!child.gameObject.name.Contains("Panel"))
                {
                    if (child.gameObject.name.Contains("(Clone)"))
                    {
                        var index = child.gameObject.name.IndexOf("(");
                        var itemName = child.gameObject.name.Substring(0, index);
                        child.gameObject.name = itemName;
                    }
                    Instantiate(child.gameObject, equipmentManager.transform.position, equipmentManager.transform.rotation).SetActive(true);
                    hotbarSlotArea.transform.Find(panelString).GetComponent<Image>().sprite = hotbarSlotArea.GetComponent<Image>().sprite;
                    EquippedItem.SetActive(false);
                    Destroy(child.gameObject);
                }
            }
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
        Item itemToCheck = slots[hotbarId - 1].GetComponent<Slot>().item;
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
}

