using System.Collections;
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
<<<<<<< HEAD
=======
        itemUse = new ItemUse();
<<<<<<< HEAD
>>>>>>> e1c192f755096b6cde23958306f517d00914df6f
        if (itemType == ItemTypes.Tool)
        {
            UseTool(Object);
        }
        if (itemType == ItemTypes.Food)
        {
            UseFood();
        }
    }

    public void UseTool(GameObject Object)
    {
        if (Object.GetComponent<Door>() != null)
=======
        if (itemType == ItemTypes.Tool)
>>>>>>> e1c192f755096b6cde23958306f517d00914df6f
        {
            if (!Object.GetComponent<Door>().open)
            {
                if (animator != null)
                {
                    animator.SetTrigger("activate");                    
                }
                Object.GetComponent<Door>().openDoor();
            }
            else
            {
                Object.GetComponent<Door>().openDoor();
            }
        }
<<<<<<< HEAD
    }

    public void UseFood()
    {
        if (itemName == "Apple")
=======
        if (itemType == ItemTypes.Food)
>>>>>>> e1c192f755096b6cde23958306f517d00914df6f
        {

        }
    }
}