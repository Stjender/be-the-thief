using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    public float Reach = 3f;
    public Hud HUD;
    public LayerMask ItemLayer;
    public string backpackTag;
    public string interactiveObjectTag;
    public KeyCode UseKey = KeyCode.F;
    public PlayerController player;
    private float itemlayer;
    private GameObject currentItem;

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
                currentItem = hit.transform.gameObject;
                if (currentItem.layer == itemlayer)
                {
                    if (currentItem.GetComponent<Item>() != null)
                    {
                        HUD.OpenMessagePanel(currentItem.GetComponent<Item>().itemName);
                    }
                    CheckInput(currentItem);
                }
                else if (currentItem.tag == backpackTag)
                {
                    HUD.OpenMessagePanel("backpack");
                    CheckInput(currentItem);
                }
                else if (currentItem.GetComponent<Item>() == null && currentItem.tag == interactiveObjectTag)
                {
                    HUD.OpenInteractMessagePanel(currentItem.name);
                    CheckInput(currentItem);
                }
                else if (currentItem.GetComponent<Item>() == null && currentItem.tag != backpackTag)
                {
                    HUD.CloseMessagePanel();
                }

            }
            else
            {
                HUD.CloseMessagePanel();
            }
        }
    }
    public void CheckInput(GameObject obj)
    {
        if (Input.GetKey(UseKey))
        {
            if (obj.layer == itemlayer)
            {
                player.inventory.PickupItem(obj);
            }
            else if (obj.tag == backpackTag)
            {
                player.inventory.AddBackpack(obj);
            }
            else if (obj.tag == interactiveObjectTag && obj.GetComponent<Door>().lockedDoor == false)
            {
                obj.GetComponent<Door>().openDoor();
            }
            else if (obj.tag == interactiveObjectTag)
            {
                player.inventory.InteractWithObject(obj);
            }
            HUD.CloseMessagePanel();
        }
    }
}