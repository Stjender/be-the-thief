using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public int id;
    public string description;
    public int slotAmount;
    public Texture2D icon;
    public GameObject slotGameObject;

    public List<GameObject> slots;

    // Start is called before the first frame update
    void Start()
    {
        slots = new List<GameObject>();

        for (int i = 0; i < slotAmount; i++)
        {
            GameObject tempSlot = Instantiate(slotGameObject);
            tempSlot.transform.SetParent(transform);
            slots.Add(tempSlot);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
