using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Atributes")]
    public AudioSource audioSound;
    public AudioClip hitBlockSound;
    public AudioClip buttonSound;
    public AudioClip buttonSoundHint;
    public AudioClip winSound;
    public AudioClip gameOverSound;

    private void Awake()
    {
        instance = this;
    }
}
