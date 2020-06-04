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

    public int ItemID;
    public string Name;
    public ItemTypes Type;
    public Sprite Icon;
    public string ItemDescription;
    public int Size;
    public bool Equipped;
    public int HotbarID = -1;

    [HideInInspector]
    public GameObject EquippedItem;
    [HideInInspector]
    public GameObject EquipmentManager;

    //NIEUW
    private ItemUse itemUse;

    public void Start()
    {
        EquipmentManager = GameObject.FindWithTag("EquipManager");
    }

    public void Update()
    {
        if (Equipped)
        {

        }
    }

    public void OnPickup()
    {
        Equipped = true;
        gameObject.SetActive(false);
    }
    //NIEUW
    public void OnUse(GameObject Object)
    {
        if (this.Type == ItemTypes.Tool)
        {
            itemUse.UseTool(this, Object);
        }
        if (this.Type == ItemTypes.Food)
        {
            itemUse.UseFood(this);
        }
    }


}
