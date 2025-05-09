using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance; 

    [SerializeField] private TextMeshProUGUI waterLeft;   //UI element to display remaining shots
    [SerializeField] private TextMeshProUGUI ducksLeft;   //UI element to display remaining ducks
    [SerializeField] private TextMeshProUGUI elevation;   //UI element to display elevation angle
    [SerializeField] private TextMeshProUGUI horizontal;  //UI element to display horizontal angle
    [SerializeField] private Slider powerSlider;          //UI slider to represent power

    private void Awake()
    {
        instance = this; 
    }

    //updates the number of shots left on the UI
    public void setWaterLeft(int value)
    {
        waterLeft.SetText("Shots: " + value);
    }

    //updates the number of ducks left on the UI
    public void setDucksLeft(int value)
    {
        ducksLeft.SetText("Yellow Ducks: " + value);
    }

    //updates the elevation angle on the UI
    public void setElevation(float value)
    {
        elevation.SetText("Elevation: " + Mathf.RoundToInt(value) + "°");
    }

    //updates the horizontal angle on the UI
    public void setHorizontal(float value)
    {
        horizontal.SetText("Horizontal: " + Mathf.RoundToInt(value) + "°");
    }

    //updates the power slider to represent the power level
    public void setPower(float value)
    {
        powerSlider.value = value;
    }
}