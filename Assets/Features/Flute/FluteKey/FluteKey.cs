using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FluteKey : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Flags]
    public enum id
    {
        One = 1,
        Two = 2,
        Three = 4,
        Four = 8
    }
    
    [SerializeField] private id mask;
    [SerializeField] private Flute flute;

    public void OnPointerDown(PointerEventData eventData)
    {
        flute.crntKeyMask = flute.crntKeyMask | mask;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        flute.crntKeyMask = flute.crntKeyMask & ~mask;
    }
}