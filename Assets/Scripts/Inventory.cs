using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int hotbarSlots;
    public GameObject[] slots;
    public SlotHolder slotHolder;
    public Hud hud;
    public LoadHudSlots inventorySlots;
    public GameObject equipmentManager;
    public KeyCode dropKey = KeyCode.G;
    public KeyCode interactObjectButton = KeyCode.Mouse0;

    private List<KeyCode> hotbarcodes;
    private int hotbarKey = 1;
    private int lastHotbarPress = 1;
    private GameObject EquippedItem = null;
    private bool equipped = false;
    private ObjectCheck objectChecker;


    private void Start()
    {
        slots = new GameObject[hotbarSlots];
        InitializeHotbar();
        for (int i = 0; i < hotbarSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        CheckHotbarInput();
        CheckDrop();
    }

    public void PickupItem(GameObject item)
    {
        if (CheckIfItemEquipped() && EquippedItem.GetComponent<Item>().Type != ItemTypes.Tool)
        {
            Debug.Log("not tool");
            return;
        }
        else if (CheckIfItemEquipped() && EquippedItem.GetComponent<Item>().Type == ItemTypes.Tool)
        {
            Debug.Log("is tool");
            UnEquipItem();
        }
        PutItemInHotbar(item);
    }
    private void CheckHotbarInput()
    {
        if (!equipped)
        {
            foreach (var key in hotbarcodes)
            {
                if (Input.GetKey(key))
                {
                    hotbarKey = Convert.ToInt32(key.ToString().Substring(5));
                    if (GetItemByHotbarId(hotbarKey) != null)
                    {
                        EquipItem(hotbarKey - 1, GetItemByHotbarId(hotbarKey));
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
            var hotbarId = EquippedItem.GetComponent<Item>().HotbarID + 1;
            Debug.Log(hotbarId);
            string panelString = "Panel";
            if (CheckIfItemEquipped())
            {
                panelString += " (" + hotbarId + ")";
            }
            else return;
            Debug.Log(panelString);
            foreach (Transform child in slotHolder.transform.Find(panelString))
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
                    slotHolder.transform.Find(panelString).GetComponent<Image>().sprite = slotHolder.GetComponent<Image>().sprite;
                    EquippedItem.SetActive(false);
                    equipped = false;
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
    private void EquipItem(int slotnumber, Item item)
    {
        if (slots[slotnumber].GetComponent<Slot>().Item != null)
        {
            var itemName = slots[slotnumber].GetComponent<Slot>().Item.name;
            if (slots[slotnumber].GetComponent<Slot>().Item.name.Contains("(Clone)"))
            {
                var index = slots[slotnumber].GetComponent<Slot>().Item.name.IndexOf("(");
                itemName = slots[slotnumber].GetComponent<Slot>().Item.name.Substring(0, index);
            }
            Debug.Log(slots[slotnumber].GetComponent<Slot>().Item.name);
            EquippedItem = equipmentManager.transform.Find(itemName).gameObject;
            Debug.Log(slotnumber);
            EquippedItem.GetComponent<Item>().HotbarID = slotnumber;
            Destroy(EquippedItem.gameObject.GetComponent<Rigidbody>());
            equipmentManager.transform.Find(EquippedItem.name).gameObject.SetActive(true);
            lastHotbarPress = slotnumber;
            equipped = true;
        }
    }
    private Item GetItemByHotbarId(int hotbarId)
    {
        if (slots[hotbarId - 1].GetComponent<Slot>().Item != null)
        {
            return slots[hotbarId - 1].GetComponent<Slot>().Item.GetComponent<Item>();
        }
        else return null;
    }
    private void UnEquipItem()
    {
        EquippedItem.SetActive(false);
        equipped = false;
        hotbarKey = 0;
    }
    private void InteractWithObject()
    {
        if (Input.GetKey(interactObjectButton) && EquippedItem != null)
        {
            switch (EquippedItem.GetComponent<Item>().Type)
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
    private void PutItemInHotbar(GameObject item)
    {
        for (int i = 0; i < hotbarSlots; i++)
        {
            if (slots[i].GetComponent<Slot>().Item == null)
            {
                item.GetComponent<Item>().HotbarID = i;
                slots[i].GetComponent<Slot>().ItemtoSlot(item.GetComponent<Item>());
                slots[i].GetComponent<Slot>().GetComponent<Image>().sprite = item.GetComponent<Item>().Icon;
                item.transform.SetParent(slots[i].transform);
                item.GetComponent<Item>().OnPickup();
                Debug.Log(i);
                EquipItem(i, item.GetComponent<Item>());
                return;
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
    private void UseItem()
    {
        EquippedItem.GetComponent<Item>().OnUse(objectChecker.currentItem);
    }
}
