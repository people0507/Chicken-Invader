using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //background
    [SerializeField] private AudioSource backgroundAudioSource;
    //ship
    [SerializeField] private AudioSource fireAudioSource;
    [SerializeField] private AudioSource levelUpAudioSource;
    [SerializeField] private AudioSource shipDeadAudioSource;
    //enemy
    [SerializeField] private AudioSource chickenHurtAudioSource;
    [SerializeField] private AudioSource chickenDeathAudioSource;
    [SerializeField] private AudioSource eggAudioSource;
    [SerializeField] private AudioSource eggBreakAudioSource;
    [SerializeField] private AudioSource rockHurtAudioSource;
    [SerializeField] private AudioSource rockDeathAudioSource;
    [SerializeField] private AudioSource eatAudioSource;

    //ackground
    public AudioClip backgroundClip;
    public AudioClip gameWinClip;
    public AudioClip gameOverClip;
    //ship
    public AudioClip fireClip;
    public AudioClip levelUpAudioClip;
    public AudioClip shipDeadAudioClip;
    //enemy
    public AudioClip chickenHurtAudioClip;
    public AudioClip chickenDeathAudioClip;
    public AudioClip eggClip;
    public AudioClip eggBreakClip;
    public AudioClip rockHurtAudioClip;
    public AudioClip rockDeathAudioClip;
    public AudioClip eatClip;


    // Start is called before the first frame update
    void Start()
    {
        PlayBackground(backgroundClip);
    }

    public void PlayBackground(AudioClip clip)
    {
        backgroundAudioSource.clip = clip;
        backgroundAudioSource.Play();
    }

    public void PlayFire(AudioClip clip)
    {
        fireAudioSource.clip = clip;
        fireAudioSource.PlayOneShot(clip);
    }

    public void PlayLevelUp(AudioClip clip)
    {
        levelUpAudioSource.clip = clip;
        levelUpAudioSource.PlayOneShot(clip);
    }

    public void PlayShipDead(AudioClip clip)
    {
        shipDeadAudioSource.clip = clip;
        shipDeadAudioSource.PlayOneShot(clip);
    }

    public void PlayChickenHurt(AudioClip clip)
    {
        chickenHurtAudioSource.clip = clip;
        chickenHurtAudioSource.PlayOneShot(clip);
    }

    public void PlayChickenDeath(AudioClip clip)
    {
        chickenDeathAudioSource.clip = clip;
        chickenDeathAudioSource.PlayOneShot(clip);
    }

    public void PlayEgg(AudioClip clip)
    {
        eggAudioSource.clip = clip;
        eggAudioSource.PlayOneShot(clip);
    }

    public void PlayEggBreak(AudioClip clip)
    {
        eggBreakAudioSource.clip = clip;
        eggBreakAudioSource.PlayOneShot(clip);
    }

    public void PlayEat(AudioClip clip)
    {
        eatAudioSource.clip = clip;
        eatAudioSource.PlayOneShot(clip);
    }

    public void PlayRockHurt(AudioClip clip)
    {
        rockHurtAudioSource.clip = clip;
        rockHurtAudioSource.PlayOneShot(clip);
    }

    public void PlayRockDeath(AudioClip clip)
    {
        rockDeathAudioSource.clip = clip;
        rockDeathAudioSource.PlayOneShot(clip);
    }

    public void StopBackgroundAudio()
    {
        backgroundAudioSource.Stop();
    }

    public void ContinueBackgroundAudio()
    {
        backgroundAudioSource.UnPause();
    }
}
