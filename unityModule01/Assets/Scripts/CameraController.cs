using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject activeCharacter;

    // Start is called before the first frame update
    void Start()
    {
        SetActiveCharacter(GameObject.FindGameObjectWithTag("Character1"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveCharacter(GameObject.FindGameObjectWithTag("Character1"));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveCharacter(GameObject.FindGameObjectWithTag("Character2"));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveCharacter(GameObject.FindGameObjectWithTag("Character3"));
        }

        if (activeCharacter != null)
        {
            Vector3 newPosition = activeCharacter.transform.position;
            newPosition.x = 7;
            newPosition.y += activeCharacter.transform.localScale.y * 0.66f;
            transform.position = newPosition;
            transform.LookAt(activeCharacter.transform);
        }
    }

    void SetActiveCharacter(GameObject character)
    {
        if (character != null)
        {
            activeCharacter = character;
        }
    }
}
