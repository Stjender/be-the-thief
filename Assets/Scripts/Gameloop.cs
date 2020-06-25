using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameloop : MonoBehaviour
{
    private int currentLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        SetupLevel1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextLevel()
    {
        currentLevel++;
        switch (currentLevel)
        {
            case 2:
                SetupLevel2();
                break;


            default:
                break;
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void SetupLevel1()
    {

    }

    void SetupLevel2()
    {

    }
}
