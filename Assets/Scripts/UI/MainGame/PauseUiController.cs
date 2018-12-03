using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseUiController : MonoBehaviour
{
    public AudioMixer MainMixer;
    public Slider SfxSlider;
    public Slider MusicSlider;
    public Slider MasterSlider;

    public void OnSfxValueChanged()
    {
        MainMixer.SetFloat("SFXVolume", SfxSlider.value);
    }

    public void OnMasterValueChanged()
    {
        MainMixer.SetFloat("MasterVolume", MasterSlider.value);
    }

    public void OnButtonQuitClick()
    {
        Debug.Log("Quitting game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
