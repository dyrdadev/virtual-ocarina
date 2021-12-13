using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

public class Flute : MonoBehaviour
{
    //[HideInInspector] public FluteKey.id crntKeyMask = 0;
    private int crntAudioSourceIndex;

    [SerializeField] private float lowerThreshold;
    [SerializeField] private float upperThreshold;

    [SerializeField] private AudioSource[] audiosources;

    [SerializeField] private Image blowIndicator;

    public void Update()
    {
        // ...
        
        
        var tempColor = blowIndicator.color;
        tempColor.a = GetCurrentVolume();
        blowIndicator.color = tempColor;
    }

    private void SoloAudioSource(int index)
    {
        // ...
    }

    private float GetCurrentVolume()
    {
        // ...
        return 0;
    }
}