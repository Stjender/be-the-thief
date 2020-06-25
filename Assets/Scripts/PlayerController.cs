using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Inventory inventory;
    public Hud Hud;
    public Collider PlayerCollider;
    public Gameloop gameloop;

    private void OnTriggerEnter(Collider other)
    {
        /*PlayerPrefs.SetFloat("level", PlayerPrefs.GetFloat("level") + 1);
        SceneManager.LoadScene("BaseLevel");*/
        gameloop.GetAllItemsCollected();
    }
}