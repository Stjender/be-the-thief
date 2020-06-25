using System;
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
    public GameObject emptyItem;
    public KeyCode dropKey = KeyCode.G;
    public KeyCode interactObjectButton = KeyCode.F;
    public KeyCode inventoryKey = KeyCode.E;
    public KeyCode dropBackpackKey = KeyCode.Q;

    private GameObject backpackObject;

    private List<KeyCode> hotbarcodes;
    private GameObject EquippedItem;
    private GameObject currentSlot;



    private void Start()
    {
        EquippedItem = emptyItem;
        hotbarSlots = new List<GameObject>();
        InitializeHotbar();
        for (int i = 0; i < amountHotbarSlots; i++)
        {
            GameObject tempSlot = Instantiate(slotGameObject);
            tempSlot.transform.SetParent(hotbarSlotArea.transform);
            hotbarSlots.Add(tempSlot);
        }
        currentSlot = hotbarSlotArea.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EquippedItem.GetComponent<Item>().itemName == "Hammer")
            {
                EquippedItem.GetComponent<Item>().animator.SetTrigger("strike");
            }
        }
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
        SwitchItems();
    }

    public void PickupItem(GameObject item)
    {
        if (EquippedItem.GetComponent<Item>().itemSize < 2)
        {
            PutItemInHotbar(item);
        }
    }

    private void PutItemInHotbar(GameObject item)
    {
        Item currentItem = item.GetComponent<Item>();
        foreach (var slot in hotbarSlots)
        {
            Slot slotToCheck = slot.GetComponent<Slot>();
            if (slotToCheck.item == null)
            {
                slotToCheck.SetItem(currentItem);

                item.transform.SetParent(slotToCheck.transform);
                currentSlot = slot;
                currentItem.OnPickup();
                EquipItem(currentItem);
                return;
            }
        }
    }

    private void EquipItem(Item item)
    {
        if (EquippedItem != null || item == null)
        {
            EquippedItem.SetActive(false);
            EquippedItem = emptyItem;
        }
        if (item != null)
        {
            foreach (var equipmentitem in equipmentManager.transform.GetComponentsInChildren<Item>(true))
            {
                if (equipmentitem.itemID == item.itemID)
                {
                    EquippedItem = equipmentitem.gameObject;
                    equipmentitem.gameObject.SetActive(true);
                }
            }
        }
    }

    private void CheckHotbarInput()
    {
        if (EquippedItem.GetComponent<Item>().itemSize < 2)
            foreach (var key in hotbarcodes)
            {
                if (Input.GetKey(key))
                {
                    currentSlot = hotbarSlotArea.transform.GetChild(Convert.ToInt32(key.ToString().Replace("Alpha", "")) - 1).gameObject;
                    Item itemToEquip = currentSlot.GetComponent<Slot>().item;
                    EquipItem(itemToEquip);
                }
            }
    }

    private void CheckDrop()
    {
        if (CheckIfItemEquipped())
        {
            Instantiate(currentSlot.GetComponent<Slot>().item.gameObject, EquippedItem.transform.position, EquippedItem.transform.rotation).SetActive(true);
            EquippedItem.SetActive(false);
            EquippedItem = emptyItem;
            Destroy(currentSlot.GetComponent<Slot>().item.gameObject);
            currentSlot.GetComponent<Slot>().SetItem(null);
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
    public void InteractWithObject(GameObject gameobject)
    {
        if (EquippedItem != null)
        {
            if (!gameobject.GetComponent<Door>().lockedDoor)
            {
                if (gameobject.GetComponent<Door>().name.Contains("Frontdoor"))
                {
                    if (EquippedItem.GetComponent<Item>().itemName == "Credit Card")
                    {
                        EquippedItem.GetComponent<Item>().OnUse(gameobject);
                    }
                }
                else
                {
                    EquippedItem.GetComponent<Item>().OnUse(gameobject);
                }
            }
            else
            {
                if (EquippedItem.GetComponent<Item>().itemName == "Screwdriver")
                {
                    EquippedItem.GetComponent<Item>().OnUse(gameobject);
                    gameobject.GetComponent<Door>().lockedDoor = false;
                }
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
        backpack.transform.position = equipmentManager.transform.position;
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
        if (inventorySlotArea.gameObject.activeSelf)
        {
            List<Slot> selectedSlots = new List<Slot>();

            List<Slot> returnedHotberSlots = GetSelectedSlots(hotbarSlotArea.transform);
            List<Slot> returnedInventorySlots = GetSelectedSlots(inventorySlotArea.transform);
            selectedSlots.AddRange(returnedHotberSlots);
            selectedSlots.AddRange(returnedInventorySlots);

            if (selectedSlots.Count >= 2)
            {
                if (EquippedItem.GetComponent<Item>().itemSize < 2)
                {
                    Slot firstSlot = selectedSlots[0];
                    Slot secondSlot = selectedSlots[1];
                    Item itemToSet = firstSlot.item;
                    firstSlot.SetItem(secondSlot.item);
                    secondSlot.SetItem(itemToSet);

                    EquipItem(currentSlot.GetComponent<Slot>().item);
                }
                DeselectAllSlots(hotbarSlotArea.transform);
                DeselectAllSlots(inventorySlotArea.transform);
            }
        }
    }

    private List<Slot> GetSelectedSlots(Transform slotArea)
    {
        List<Slot> selectedSlots = new List<Slot>();
        for (int i = 0; i < slotArea.childCount; i++)
        {
            Transform currentChild = slotArea.GetChild(i);
            Slot currentChildSlot = currentChild.GetComponent<Slot>();
            if (currentChildSlot.isSelected)
            {
                selectedSlots.Add(currentChildSlot);
            }
        }
        return selectedSlots;
    }

    private void DeselectAllSlots(Transform slotArea)
    {
        for (int i = 0; i < slotArea.childCount; i++)
        {
            Transform currentChild = slotArea.GetChild(i);
            Slot currentChildSlot = currentChild.GetComponent<Slot>();
            currentChildSlot.isSelected = false;
        }
    }
}