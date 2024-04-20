using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private short colorNumber;
    protected Color currentColor = Color.white;
    protected bool isTriggered = false;

    private void Start()
    {
        switch (colorNumber)
        {
            case 0:
                ChangeColor(Color.red);
                break ;
            case 1:
                ChangeColor(Color.blue);
                break ;
            case 2:
                ChangeColor(Color.yellow);
                break ;
            default:
                break ;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (CanBeTriggered(other))
            TriggerSwitch();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }

    protected virtual bool CanBeTriggered(Collider other)
    {
        if (!isTriggered && other.tag.Contains("Character"))
        {
            if (currentColor == Color.white)
                return (true);
            else
            {
                string characterLayer = LayerMask.LayerToName(other.gameObject.layer);
                string characterColor = characterLayer.Replace("Character", "");
                return ColorMatches(characterColor);
            }
        }
        return (false);
    }

    private bool ColorMatches(string characterColor)
    {
        if (currentColor == Color.red && characterColor == "Red")
            return true;
        if (currentColor == Color.blue && characterColor == "Blue")
            return true;
        if (currentColor == Color.yellow && characterColor == "Yellow")
            return true;
        return false;
    }

    protected virtual void ChangeColor(Color newColor)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            currentColor = newColor;
            if (newColor == Color.red)
                renderer.material = materials[0];
            else if (newColor == Color.blue)
                renderer.material = materials[1];
            else if (newColor == Color.yellow)
                renderer.material = materials[2];
        }
    }

    protected virtual void TriggerSwitch()
    {
        isTriggered  = true;
    }
}
