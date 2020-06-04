using UnityEngine;

public class ObjectCheck : MonoBehaviour
{
    public float Reach = 10f;
    public Hud HUD;
    public LayerMask ItemLayer;
    public string backpackTag;
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
                    CheckInput();
                }
                else if (currentItem.tag == backpackTag)
                {
                    HUD.OpenMessagePanel("backpack");
                    CheckInput();
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
            if (currentItem.layer == itemlayer)
            {
                player.inventory.PickupItem(currentItem);
            }
            else if (currentItem.tag == backpackTag)
            {
                player.inventory.AddBackpack(currentItem);
            }
            HUD.CloseMessagePanel();
        }
    }
}