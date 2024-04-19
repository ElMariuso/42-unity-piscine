using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorSwitch : SwitchManager
{
    [SerializeField] private GameObject[] doorsToControl;
    [SerializeField] private bool shouldTakeColor;

    private void OnCollisionEnter(Collision other)
    {
        if (shouldTakeColor)
        {

        }
    }

    private void OpenDoors()
    {
        foreach (var door in doorsToControl)
            door.SetActive(!door.activeSelf);
    }
}
