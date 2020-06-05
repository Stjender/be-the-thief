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

    //moet nog naar gekeken worden
    public void SlotClick()
    {
        this.isSelected = !this.isSelected;

        List<GameObject> slots = new List<GameObject>();
        Transform parent = transform.parent;

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform tempChild = parent.GetChild(i);
            Slot tempChildSlot = tempChild.GetComponent<Slot>();
            if (tempChildSlot.isSelected && tempChild != transform)
            {
                Item currentItem = item;
                item = tempChild.GetComponent<Slot>().item;
                tempChildSlot.item = currentItem;
                isSelected = false;
                tempChildSlot.isSelected = false;
                ShowItemInSlot();
                tempChildSlot.ShowItemInSlot();
            }
        }
    }
}