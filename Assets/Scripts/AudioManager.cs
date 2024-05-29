using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip Menu_Excited;
    public AudioClip Menu_Intense;
    public AudioClip Main_Game;
    public AudioClip Death;
    public AudioClip Shoot;
    public AudioClip Hit;
    public AudioClip Collect;
    public AudioClip BakaShoot;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float menuExcitedVolume = 1.0f;
    [Range(0f, 1f)]
    public float menuIntenseVolume = 1.0f;
    [Range(0f, 1f)]
    public float mainGameVolume = 1.0f;
    [Range(0f, 1f)]
    public float deathVolume = 1.0f;
    [Range(0f, 1f)]
    public float shootVolume = 1.0f;
    [Range(0f, 1f)]
    public float hitVolume = 1.0f;
    [Range(0f, 1f)]
    public float collectVolume = 1.0f;
    [Range(0f, 1f)]
    public float bakaShootVolume = 1.0f;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            MusicSource.loop = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        MusicSource.Stop();
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            MusicSource.clip = Menu_Excited;
            MusicSource.volume = menuExcitedVolume;
        }
        else
        {
            MusicSource.clip = Menu_Intense;
            MusicSource.volume = menuIntenseVolume;
        }
        MusicSource.Play();
    }

    public void FromMenuToGame()
    {
        MusicSource.Stop();
        MusicSource.clip = Main_Game;
        MusicSource.volume = mainGameVolume;
        MusicSource.Play();
    }

    public void PlayerDeath()
    {
        MusicSource.Stop();
        SFXSource.PlayOneShot(Death, deathVolume);
    }

    public void PlayShoot()
    {
        SFXSource.PlayOneShot(Shoot, shootVolume);
    }

    public void PlayHit()
    {
        SFXSource.PlayOneShot(Hit, hitVolume);
    }

    public void PlayCollect()
    {
        SFXSource.PlayOneShot(Collect, collectVolume);
    }

    public void PlayBakaShoot()
    {
        SFXSource.PlayOneShot(BakaShoot, bakaShootVolume);
    }
}
