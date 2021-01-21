using System;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneInput : MonoBehaviour
{
    public static float MicLoudness => _micLoudness;
    private static float _micLoudness;

    private bool _isInitialized;
    private AudioSource _AudioSource;
    private int _sampleWindow = 128;

    private void InitializeMicrophone()
    {
        throw new NotImplementedException();
    }

    private float RootMeanSquare()
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        _micLoudness = RootMeanSquare();
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