using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private bool isEnergy = false;

    private void Awake()
    {
        if (isEnergy)
        {
            BarScript barScript = GetComponent<BarScript>();
            if (GameManager.Instance != null)
                GameManager.Instance.SetupEnergyBar(barScript);
        }
    }

    public void SetMaxValue(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetValue(int value)
    {
        slider.value = value;
    }
}
