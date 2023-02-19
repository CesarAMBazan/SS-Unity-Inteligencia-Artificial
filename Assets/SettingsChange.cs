using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsChange : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject _slider;
    private const string _sliderMouseSensitivity = "sliderMouseSensitivity";

    public void SliderChanged(Slider slider, float value)
    {
        switch (slider.name)
        {
            case _sliderMouseSensitivity:
                player.GetComponent<FirstPersonController>().mouseSensitivity = slider.value / 100;
                break;
        }
    }
}
