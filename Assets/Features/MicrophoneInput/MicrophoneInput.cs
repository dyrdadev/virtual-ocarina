﻿using UnityEngine;
using UnityEngine.UI;

public class MicrophoneInput : MonoBehaviour
{
    public static float MicrophoneLoudness => _microphoneLoudness;
    private static float _microphoneLoudness;
    private bool _isInitialized;
    private AudioSource _audioSource;
    private int _sampleWindow = 128;

    private void InitializeMicrophone()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = Microphone.Start(null, true, 10, 44100);
        _audioSource.Play();
    }

    private float RootMeanSquare()
    {
        if (_isInitialized)
        {
            float rms = 0;
            var waveData = new float[_sampleWindow];
            var micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
            if (micPosition < 0)
            {
                return 0;
            }

            _audioSource.clip.GetData(waveData, micPosition);
            
            rms = 0;
            var counter = 0;
            for (var i = 0; i < _sampleWindow; i++)
            {
                var wavePeak = waveData[i] * waveData[i];
                rms += wavePeak;
                counter++;
            }

            rms = Mathf.Sqrt(rms / counter);

            return rms;
        }

        return 0;
    }

    private void Update()
    {
        _microphoneLoudness = RootMeanSquare();
    }

    private void StopMicrophone()
    {
        Microphone.End(null);
    }

    private void OnEnable()
    {
        InitializeMicrophone();
        _isInitialized = true;
    }

    private void OnDisable()
    {
        StopMicrophone();
    }

    private void OnDestroy()
    {
        StopMicrophone();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (!_isInitialized)
            {
                InitializeMicrophone();
                _isInitialized = true;
            }
        }

        if (!focus)
        {
            StopMicrophone();
            _isInitialized = false;
        }
    }
}