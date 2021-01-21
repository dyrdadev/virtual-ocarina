using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;
using DyrdaIo.Singleton;

public class Flute : MonoBehaviour
{
    [HideInInspector] public FluteKey.id crntKeyMask = 0;
    private int crntAudioSourceIndex;

    [SerializeField] private float lowerThreshold;
    [SerializeField] private float upperThreshold;

    [SerializeField] private AudioSource[] audiosources;

    [SerializeField] private Image blowIndicator;

    public void Update()
    {
        switch (crntKeyMask)
        {
            case FluteKey.id.One | FluteKey.id.Two | FluteKey.id.Three | FluteKey.id.Four:
                SoloAudioSource(0);
                break;
            case FluteKey.id.One | FluteKey.id.Two | FluteKey.id.Three:
                SoloAudioSource(1);
                break;
            case FluteKey.id.One | FluteKey.id.Two | FluteKey.id.Four:
                SoloAudioSource(2);
                break;
            case FluteKey.id.One | FluteKey.id.Two:
                SoloAudioSource(3);
                break;
            case FluteKey.id.One | FluteKey.id.Three:
                SoloAudioSource(4);
                break;
            case FluteKey.id.One:
                SoloAudioSource(5);
                break;
            case FluteKey.id.Four:
                SoloAudioSource(6);
                break;
            case 0:
                SoloAudioSource(7);
                break;
            default:
                SoloAudioSource(7);
                break;
        }

        Debug.Log(GetCurrentVolume() + " / " + MicrophoneInput.MicLoudness);
        var tempColor = blowIndicator.color;
        tempColor.a = GetCurrentVolume();
        blowIndicator.color = tempColor;
    }

    private void SoloAudioSource(int index)
    {
        for (var i = 0; i < audiosources.Length; i++)
        {
            if (audiosources[i] != null)
            {
                if (i == index)
                {
                    audiosources[i].mute = false;
                    if (MicrophoneInput.MicLoudness > lowerThreshold)
                    {
                        audiosources[i].volume = GetCurrentVolume();
                    }
                    else
                    {
                        audiosources[i].volume = 0;
                    }
                }
                else
                {
                    audiosources[i].mute = true;
                }
            }
        }
    }

    private float GetCurrentVolume()
    {
        return Mathf.Clamp((MicrophoneInput.MicLoudness - lowerThreshold) / (upperThreshold - lowerThreshold), 0.0f,
            1.0f);
    }
}