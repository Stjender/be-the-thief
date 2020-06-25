using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameloop : MonoBehaviour
{
    public GameObject Objects;
    public GameObject Exit;

    public GameObject[] windows;
    public PlayerController player;
    public Door Frontdoor;

    // Start is called before the first frame update
    void Start()
    {
        windows = GameObject.FindGameObjectsWithTag("Glass2");
        foreach (GameObject glass in windows)
        {
            glass.SetActive(false);
        }

        //Voor het testen!!!!!!
        PlayerPrefs.SetFloat("level", 4);


        if (PlayerPrefs.GetFloat("level") == 0)
        {
            PlayerPrefs.SetFloat("level", 1);
        }
        LoadLevel(PlayerPrefs.GetFloat("level"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel(float currentLevel)
    {
        switch (currentLevel)
        {
            case 1:
                SetupLevel1();
                break;

            case 2:
                SetupLevel2();

                break;
            case 3:
                SetupLevel3();
                break;
            case 4:

                SetupLevel4();
                break;

            default:
                break;
        }
    }    

    void SetupLevel1()
    {
        GameObject obj = Objects.transform.FindChild("creditcard").gameObject;
        player.inventory.PickupItem(obj);
        Frontdoor.lockedDoor = false;
    }

    void SetupLevel2()
    {
        GameObject obj = Objects.transform.FindChild("screwdriver").gameObject;
        player.inventory.PickupItem(obj);
        Frontdoor.lockedDoor = true;
    }

    void SetupLevel3()
    {
        GameObject obj = Objects.transform.FindChild("hammer").gameObject;
        player.inventory.PickupItem(obj);
        Frontdoor.lockedDoor = true;
    }

    void SetupLevel4()
    {
        foreach (GameObject glass in GameObject.FindGameObjectsWithTag("Glass1"))
        {
            glass.SetActive(false);
        }
        foreach (GameObject glass in GameObject.FindGameObjectsWithTag("Glass2"))
        {
            glass.SetActive(true);
        }
    }

    void ResetScene()
    {
        
    }
}
