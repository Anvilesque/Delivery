using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Music
{
    [HideInInspector] public AudioSource source;
    public AudioClip clip;
    public float bpm;
}
