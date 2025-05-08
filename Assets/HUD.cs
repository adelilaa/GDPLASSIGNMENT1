using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    [SerializeField] private TextMeshProUGUI waterLeft;
    [SerializeField] private TextMeshProUGUI ducksLeft;
    [SerializeField] private TextMeshProUGUI elevation;
    [SerializeField] private TextMeshProUGUI horizontal;
    [SerializeField] private Slider powerSlider;

    private void Awake()
    {
        instance = this;
    }

    public void setWaterLeft(int value)
    {
        waterLeft.SetText("Shots: " + value);
    }

    public void setDucksLeft(int value)
    {
        ducksLeft.SetText("Yellow Ducks: " + value);
    }

    public void setElevation(float value)
    {
        elevation.SetText("Elevation: " + Mathf.RoundToInt(value) + "°");
    }

    public void setHorizontal(float value)
    {
        horizontal.SetText("Horizontal: " + Mathf.RoundToInt(value) + "°");
    }

    public void setPower(float value)
    {
        powerSlider.value = value;
    }
}