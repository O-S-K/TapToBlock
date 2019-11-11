using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Atributes")]
    public AudioSource audioSound;
    public AudioClip splashFallSound;
    public AudioClip buttonSound;
    public AudioClip buttonSoundSkip;
    public AudioClip winGameSound;
    public AudioClip gameOverSound;

    private void Awake()
    {
        instance = this;
    }
}
