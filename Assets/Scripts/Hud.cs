using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Inventory Inventory;

    public GameObject MessagePanel;

    public Text InteractionText;

    public string DefaultInteractionText;

    void Start()
    {
        
    }

    public void OpenMessagePanel(string text)
    {
        InteractionText.text = DefaultInteractionText;
        MessagePanel.SetActive(true);
        text = " " + text;
        InteractionText.text += text;
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }
}
