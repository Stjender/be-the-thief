using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadHudSlots : MonoBehaviour
{
    public GameObject HUD;
    public Image Slot;
    public short SlotAmount;
    public short SlotAmountX;
    public short SlotAmountY;
    public float SpaceBetweenSlots;
    public Vector3 InventoryPanelVector3;

    private List<float> GetListOfAxisPos(Vector2 slotSize, Vector3 startVec, int numberOfSlots, char axis)
    {
        List<float> posList = new List<float>();

        float startPos = 0;
        float slotSide = 0;

        if (axis == 'x')
        {
            startPos = startVec.x;
            slotSide = slotSize.x;
        }
        else if (axis == 'y')
        {
            startPos = startVec.y;
            slotSide = slotSize.y;
        }

        if (numberOfSlots % 2 == 0)
        {
            for (int i = 0; i < numberOfSlots / 2; i++)
            {
                float spaceBetweenThisSlot = (float)(i + 0.5) * SpaceBetweenSlots;
                float amountOfSlots = (float)(i + 0.5) * slotSide;

                posList.Add(startPos + spaceBetweenThisSlot + amountOfSlots);
                posList.Add(startPos - spaceBetweenThisSlot - amountOfSlots);
            }
        }
        else
        {
            int newNumberOfSlots = numberOfSlots - 1;
            posList.Add(startPos);
            for (int i = 1; i <= newNumberOfSlots / 2; i++)
            {
                float spaceBetweenThisSlot = i * SpaceBetweenSlots;
                float amountOfSlots = i * slotSide;

                posList.Add(startPos + spaceBetweenThisSlot + amountOfSlots);
                posList.Add(startPos - spaceBetweenThisSlot - amountOfSlots);
            }
        }

        return posList;
    }

    private void GenerateSlots(List<float> ListXPos, List<float> ListYPos)
    {
        int i = 0;
        int tempi = 0;
        foreach (var Xpos in ListXPos)
        {
            foreach (var Ypos in ListYPos)
            {
                i++;
                InventoryPanelVector3.x += tempi;
                Image tempSlot = Instantiate(Slot);
                tempSlot.transform.position = InventoryPanelVector3;
                tempSlot.transform.SetParent(HUD.transform);
                tempSlot.name = "Slot" + i;
                tempi += 50;
            }
        }
    }

}
