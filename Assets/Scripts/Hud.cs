using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Inventory Inventory;

    public GameObject MessagePanel;

    public Text InteractionText;

    public string DefaultInteractionText;

    public GameObject InfoButton;

    public TextMeshProUGUI TimeText;

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

    public void OpenInteractMessagePanel(string text)
    {
        InteractionText.text = "Press F to interact with";
        MessagePanel.SetActive(true);
        text = " " + text;
        InteractionText.text += text;
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }

    public void OpenInfoPanel(string levelInfo)
    {
        Cursor.lockState = CursorLockMode.None;
        InfoButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = levelInfo;
        InfoButton.SetActive(true);
    }

    public void CloseInfoPanel()
    {
        InfoButton.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}