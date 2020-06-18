using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int Id;

    //moet nog naar gekeken worden
    public bool isSelected;
    public Sprite baseImage;

    public void ShowItemInSlot()
    {
        if (item != null)
        {
            this.GetComponent<Image>().sprite = item.icon;
        }
        else
        {
            this.GetComponent<Image>().sprite = baseImage;
        }
    }

    public void SetItem(Item itemToSet)
    {
        item = itemToSet;
        ShowItemInSlot();
        if (itemToSet != null)
        {
            Debug.Log(itemToSet.itemName);
            itemToSet.transform.SetParent(transform);
        }
    }

    //moet nog naar gekeken worden
    public void SlotClick()
    {
        isSelected = !isSelected;
        Debug.Log("slot pressed");
    }
}