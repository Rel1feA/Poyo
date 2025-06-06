using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPlane : BasePanel
{
    public Toggle fullScreenToggle;
    public Dropdown dropdown;
    public Slider BGMSlider;
    public Slider SESlider;

    private void Start()
    {
        BGMSlider.onValueChanged.AddListener(AudioManager.Instance.ChangeBGMValue);
        SESlider.onValueChanged.AddListener(AudioManager.Instance.ChangeSEValue);
        fullScreenToggle.isOn = Screen.fullScreen;
        fullScreenToggle.onValueChanged.AddListener(GameManager.Instance.ChangeFullScreen);
        SetDropdownValue();
        SetAudioSliderValue();
        dropdown.onValueChanged.AddListener(GameManager.Instance.ChangeResolution);
        GetControl<Button>("CloseBTN").onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel("SettingPlane");
        });
    }

    public void SetDropdownValue()
    {
        switch (Screen.width)
        {
            case 1920:
                dropdown.value = 0;
                break;
            case 1280:
                dropdown.value = 1; 
                break;
            case 1024:
                dropdown.value = 2;
                break;
            default:
                break;
        }
    }

    public void SetAudioSliderValue()
    {
        AudioManager.Instance.mixer.GetFloat("BGM", out float bgmValue);
        BGMSlider.value = bgmValue;
        AudioManager.Instance.mixer.GetFloat("SE", out float seValue);
        SESlider.value = seValue;
    }
}
