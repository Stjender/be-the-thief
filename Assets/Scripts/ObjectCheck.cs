using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    public float Reach = 10f;
    public Hud HUD;
    public LayerMask ItemLayer;
    public KeyCode UseKey = KeyCode.F;
    public PlayerController player;
    private float itemlayer;
    private bool useKeyPressed = false;
    public GameObject currentItem;

    // Start is called before the first frame update
    void Start()
    {
        itemlayer = Mathf.Log(ItemLayer.value, 2);
    }

    // Update is called once per frame
    void Update()
    {
        CheckHit();
    }
    public void CheckHit()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.distance < Reach)
            {
                if (hit.transform.gameObject.layer == itemlayer)
                {
                    currentItem = hit.transform.gameObject;
                    if (currentItem != null)
                    {
                        if (currentItem.GetComponent<Item>() != null)
                        {
                            HUD.OpenMessagePanel(currentItem.GetComponent<Item>().Name);
                        }
                        CheckInput();
                    }
                }
            }
            else
            {
                HUD.CloseMessagePanel();
            }
        }
    }
    public void CheckInput()
    {
        if (Input.GetKey(UseKey))
        {
            useKeyPressed = true;
        }
        if (useKeyPressed && currentItem != null)
        {
            player.inventory.PickupItem(currentItem);
            HUD.CloseMessagePanel();
        }
        useKeyPressed = false;
    }
}

