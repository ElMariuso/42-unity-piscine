using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ActivateCharacter(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // ActivateCharacter(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // ActivateCharacter(2);
        }
    }
}
