using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        movementDirection = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        string platformLayer = LayerMask.LayerToName(gameObject.layer);
        string characterLayer = LayerMask.LayerToName(other.gameObject.layer);
        string platformColor = platformLayer.Replace("Platform", "");
        string characterColor = characterLayer.Replace("Character", "");

        if (platformColor == characterColor
            || (platformColor == "White" && other.tag.Contains("Character")))
            other.transform.SetParent(transform);
        switch (other.tag)
        {
            case "MovePlatformUp":
                movementDirection = Vector3.up;
                break ;
            case "MovePlatformDown":
                movementDirection = Vector3.down;
                break ;
            case "MovePlatformRight":
                movementDirection = Vector3.forward;
                break ;
            case "MovePlatformLeft":
                movementDirection = Vector3.back;
                break ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string platformLayer = LayerMask.LayerToName(gameObject.layer);
        string characterLayer = LayerMask.LayerToName(other.gameObject.layer);
        string platformColor = platformLayer.Replace("Platform", "");
        string characterColor = characterLayer.Replace("Character", "");

        if (platformColor == characterColor
            || (platformColor == "White" && other.tag.Contains("Character")))
        {
            GameObject charactersParent = GameObject.Find("Characters");
            if (charactersParent != null)
                other.transform.SetParent(charactersParent.transform);
            else
            other.transform.SetParent(null);
        }
    }

}
