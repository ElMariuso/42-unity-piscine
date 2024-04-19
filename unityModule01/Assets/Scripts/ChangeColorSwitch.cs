using System.Collections.Generic;
using UnityEngine;

public class ChangeColorSwitch : SwitchManager
{
    [System.Serializable]
    public struct ColorTransition
    {
        public Material currentMaterial;
        public Material newMaterial;
        public string newLayerName;
    }

    [SerializeField] private ColorTransition[] colorTransitions;
    [SerializeField] private GameObject[] objectsToChange;

    protected override void TriggerSwitch()
    {
        base.TriggerSwitch();
        RotateColors();
    }

    private void RotateColors()
    {
        foreach (GameObject obj in objectsToChange)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material currentMaterial = renderer.sharedMaterial;
                foreach (var transition in colorTransitions)
                {
                    if (currentMaterial == transition.currentMaterial)
                    {
                        ChangeColor(renderer, obj, transition.newMaterial, transition.newLayerName);
                        break ;
                    }
                }
            }
        }
    }

    private void ChangeColor(Renderer renderer, GameObject obj, Material newMaterial, string newLayerName)
    {
        renderer.material = newMaterial;
        obj.layer = LayerMask.NameToLayer(newLayerName);
    }
}
