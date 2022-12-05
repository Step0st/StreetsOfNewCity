using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    [SerializeField] private Slider sliderUI;
    [SerializeField] private TextMeshProUGUI textSliderValue;
    private string _initialText;
    void Start ()
    {
        _initialText = textSliderValue.text;
        ShowSliderValue();
    }

    public void ShowSliderValue ()
    {
        string sliderMessage = " = " + sliderUI.value;
        textSliderValue.text = _initialText + sliderMessage;
    }
}