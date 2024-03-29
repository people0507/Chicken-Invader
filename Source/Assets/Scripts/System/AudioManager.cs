using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] allAudioSources;
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
    [SerializeField] private AudioSource eggHatchAudioSource;

    //background
    public AudioClip backgroundMenuClip;
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
    public AudioClip eggHatchClip;


    // Start is called before the first frame update
    void Start()
    {
        int currenScene = SceneManager.GetActiveScene().buildIndex;
        if (currenScene == 0)
        {
            Cursor.visible = true;
            PlayBackground(backgroundMenuClip);
        }
        else
        {
            Cursor.visible = false;
            PlayBackground(backgroundClip);
        }
        allAudioSources = new AudioSource[]
        {
            backgroundAudioSource,
            fireAudioSource,
            levelUpAudioSource,
            shipDeadAudioSource,
            chickenHurtAudioSource,
            chickenDeathAudioSource,
            eggAudioSource,
            eggBreakAudioSource,
            rockHurtAudioSource,
            rockDeathAudioSource,
            eatAudioSource,
            eggHatchAudioSource
        };
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
    public void PlayEggHatch(AudioClip clip)
    {
        eggHatchAudioSource.clip = clip;
        eggHatchAudioSource.PlayOneShot(clip);
    }

    public void StopAllAudioSources()
    {
        foreach(AudioSource audio in allAudioSources)
            audio.Pause();
    }
    public void ResumeAllAudiioSources()
    {
        foreach (AudioSource audio in allAudioSources)
            audio.UnPause();
    }
}
