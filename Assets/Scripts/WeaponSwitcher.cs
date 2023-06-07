using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] objectsToSwitch;
    private int currentIndex = 0;

    void Start()
    {
        // Set the initial active object
        SetActiveObject(currentIndex);
    }

    void Update()
    {
        // Detect mouse scroll input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Scroll up
        if (scrollInput > 0f)
        {
            SwitchToNextObject();
        }
        // Scroll down
        else if (scrollInput < 0f)
        {
            SwitchToPreviousObject();
        }
    }

    void SwitchToNextObject()
    {
        currentIndex++;
        if (currentIndex >= objectsToSwitch.Length)
        {
            currentIndex = 0;
        }

        SetActiveObject(currentIndex);
    }

    void SwitchToPreviousObject()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = objectsToSwitch.Length - 1;
        }

        SetActiveObject(currentIndex);
    }

    void SetActiveObject(int index)
    {
        // Disable all objects
        foreach (GameObject obj in objectsToSwitch)
        {
            obj.SetActive(false);
        }

        // Enable the selected object
        objectsToSwitch[index].SetActive(true);
        objectsToSwitch[index].GetComponent<TestSpell>().SetUI();
    }
}
