using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorSwitch : SwitchManager
{
    [SerializeField] private GameObject[] doorsToControl;
    [SerializeField] private bool shouldTakeColor;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (shouldTakeColor)
            ChangeSwitchColorBasedOnCharacter(other);
    }

    protected override void TriggerSwitch()
    {
        base.TriggerSwitch();

        if (shouldTakeColor && currentColor != Color.white)
            OpenColorDoor();
        else
            OpenDoors();
    }

    private void OpenDoors()
    {
        foreach (var door in doorsToControl)
            door.SetActive(!door.activeSelf);
    }

    private void OpenColorDoor()
    {
        string colorTag = ColorToTag(currentColor);

        foreach (var door in doorsToControl)
        {
            if (door.tag == colorTag)
                door.SetActive(!door.activeSelf);
        }
    }

    private string ColorToTag(Color color)
    {
        if (color == Color.red)
            return "RedDoor";
        else if (color == Color.blue)
            return "BlueDoor";
        else if (color == Color.yellow)
            return "YellowDoor";
        else
            return "";
    }

    private void ChangeSwitchColorBasedOnCharacter(Collider characterCollider)
    {
        switch (characterCollider.tag)
        {
            case "Character1":
                ChangeColor(Color.red);
                break;
            case "Character2":
                ChangeColor(Color.blue);
                break;
            case "Character3":
                ChangeColor(Color.yellow);
                break;
        }
    }
}
