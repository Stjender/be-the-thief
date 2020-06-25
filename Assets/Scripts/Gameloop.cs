using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameloop : MonoBehaviour
{
    public GameObject Objects;
    public GameObject Exit;

    private GameObject[] windows;

    public PlayerController player;
    public Door Frontdoor;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        //Voor het testen!!!!!!
        PlayerPrefs.SetFloat("level", 4);


        if (PlayerPrefs.GetFloat("level") == 0)
        {
            PlayerPrefs.SetFloat("level", 1);
        }

        DisableWindows();
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

        string levelinfo = "Level 1: \r\n \r\n Manier van inbraak: Raam inslaan. \r\n Je hebt een hamer in bezit, probeer een raam in te slaan om in het huis te komen.";
        player.Hud.OpenInfoPanel(levelinfo);
    }

    void SetupLevel2()
    {
        GameObject obj = Objects.transform.FindChild("screwdriver").gameObject;
        player.inventory.PickupItem(obj);
        Frontdoor.lockedDoor = true;

        string levelinfo =  "Level 2:" + "\r\n" + "\r\n" +
                            "Voorkomen van raam inslag:" + "\r\n" +
                            "•    Leg kostbare apparatuur, zoals laptops en iPads, uit het zicht." + "\r\n" +
                            "•    Dubbelglas ruiten helpen tegen het inslaan. Ze zijn veel lastiger om in te slaan dan enkel glas ruiten." + "\r\n" +
                            "De eigenaar van het huis heeft dubbelglas gebruikt dit keer, dus je kunt niet meer dezelfde methode gebruiken." + "\r\n" +
                            "Nieuwe manier van inbraak: Flipperen." + "\r\n" +
                            "Probeer te flipperen om binnen te komen.";
        player.Hud.OpenInfoPanel(levelinfo);
    }

    void SetupLevel3()
    {

        GameObject obj = Objects.transform.FindChild("hammer").gameObject;
        player.inventory.PickupItem(obj);
        Frontdoor.lockedDoor = true;

        string levelinfo =  "Level 3:" + "\r\n" + "\r\n" +
                            "Voorkomen van flipperen:" + "\r\n" +
                            "•    Laat als het donker is en u niet thuis bent een lamp aan. Dan lijkt het alsof er iemand aanwezig is. Stel hiervoor bijvoorbeeld een automatische lichtschakelaar in." + "\r\n" +
                            "•    Sluit ramen en deuren goed af, ook als u maar even weg bent. Draai de deur ook altijd op slot." + "\r\n" +
                            "De eigenaar heeft dit keer zijn huis goed op slot gezet, je kunt dus dit keer niet meer flipperen." + "\r\n" +
                            "Nieuwe manier van inbraak: Cilinder afbreken van een deurslot." + "\r\n" +
                            "Probeer in te breken met de schroevendraaier.";
        player.Hud.OpenInfoPanel(levelinfo);
    }

    void SetupLevel4()
    {
        foreach (GameObject glass in GameObject.FindGameObjectsWithTag("Glass1"))
        {
            glass.SetActive(false);
        }
        foreach (GameObject glass in windows)
        {
            glass.SetActive(true);
        }

        string levelinfo =  "Level 4:" + "\r\n" + "\r\n" +
                            "Helaas, de eigenaar heeft zich nu volledig beveiligd tegen inbraak. " + "\r\n" +
                            "Je hebt dit keer alles bij je, kijk voor de zekerheid of je nog in kunt breken!";
        player.Hud.OpenInfoPanel(levelinfo);
    }

    void ResetScene()
    {

    }

    void DisableWindows()
    {
        windows = GameObject.FindGameObjectsWithTag("Glass2");
        foreach (GameObject glass in windows)
        {
            glass.SetActive(false);
        }
    }
}
