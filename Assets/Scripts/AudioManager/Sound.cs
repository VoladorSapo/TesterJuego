using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume = 1.0f;
    [Range(0f, 3f)]
    public float pitch = 1.0f;
    public float maxSoundDistance;
}
