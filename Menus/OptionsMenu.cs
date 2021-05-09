using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Options Menu Handler
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; // Audio Levels

    /// <summary>
    /// 
    /// Set Volume
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume", volume);
    }

    /// <summary>
    /// Set Quality
    /// </summary>
    /// <param name="qualityIndex"></param>
    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Toggle Fullscreen
    /// </summary>
    /// <param name="isFullscreen"></param>
    public void SetFullScreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }
}
