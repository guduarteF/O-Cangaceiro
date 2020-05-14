using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float volume;
    [HideInInspector]
    public AudioSource source;
    public bool loop;

    
}
