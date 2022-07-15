using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audiomixer = default;
    [SerializeField] private AudioMixerSnapshot normal = default;
    [SerializeField] private AudioMixerSnapshot paused = default;

    [SerializeField] private AudioClip[] winSounds = default;
    [SerializeField] private AudioClip[] failSounds = default;
    [SerializeField] private AudioClip[] playerJump = default;
    [SerializeField] private AudioClip[] playerAttack = default;
    [SerializeField] private AudioClip[] playerHurt = default;
    [SerializeField] private AudioClip[] playerDash = default;
    [SerializeField] private AudioClip[] playerDeath = default;
    [SerializeField] private AudioClip[] fireBall = default;
    [SerializeField] private AudioClip[] enemyHit = default;
    [SerializeField] private AudioClip trapFire, coin, heart, powerUp, openSwitch;
    [SerializeField] private AudioClip soundTrackMenu , soundTrackGame;

    [SerializeField] private AudioSource musicSource, powerUp_Coin_HeartSource, trapFireSource, fireBallSource, switchSource, winSource, failSource, playerJumpSource, playerHurtSource, playerAttackSource, playerDashSource, playerDeathSource, enemyHitSource;

    public static SoundManager _instance;
    public static SoundManager Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Debug.Log("BOOLDROP :" + PlayerPrefs.GetInt("BOOLDROP"));
    }



    private void Start()
    {        
        musicSource.clip = soundTrackMenu;        
        musicSource.Play();

        MuteAll(SaveGame.GetAudioIsOn());
        SetMusicVolume(SaveGame.GetMusic());
        SetSfxVolume(SaveGame.GetSound());        
    }

    //Restart the soundTrack
    public void RestartSound()
    {
        musicSource.clip = soundTrackGame;
        musicSource.Play();
    }

    public void SoundMenuToGame()
    {
        musicSource.clip = soundTrackGame;
        musicSource.Play();
    }

    //Play an audio from a array
    public void PlayRandomSound(AudioSource audioSource, AudioClip[] audioClips)
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length - 1)]);
    }

    //Play an audio from a single clip
    public void PlaySound(AudioSource audioSource, AudioClip audioClips)
    {
        audioSource.PlayOneShot(audioClips);
    }

    public void StopSound(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void StopSoundTrackGame()
    {
        StopSound(musicSource);
    }

    //Methods win and fail sound
    public void PlayRandomWinSound()
    {
        PlayRandomSound(winSource, winSounds);
    }

    public void PlayRandomFailSound()
    {
        PlayRandomSound(failSource, failSounds);
    }

    //Methods player sound
    public void PlayRandomPlayerJumpSound()
    {
        PlayRandomSound(playerJumpSource, playerJump);
    }

    public void PlayRandomPlayerAttackSound()
    {
        PlayRandomSound(playerAttackSource, playerAttack);
    }
    public void PlayRandomPlayerHurtSound()
    {
        PlayRandomSound(playerHurtSource, playerHurt);
    }

    public void PlayRandomPlayerDashSound()
    {
        PlayRandomSound(playerDashSource, playerDash);
    }

    public void PlayRandomPlayerDeathSound()
    {
        PlayRandomSound(playerDeathSource, playerDeath);
    }

    //Methods enemy and  environment
    public void PlayRandomEnemyHitSound()
    {
        PlayRandomSound(enemyHitSource, enemyHit);
    }

    public void PlayTrapFireSound()
    {
        PlaySound(trapFireSource, trapFire);
    }

    public void PlayFireBallSound()
    {
        PlayRandomSound(fireBallSource, fireBall);
        Debug.Log(fireBall); 
    }

    public void PlayOpenSwitch()
    {
        PlaySound(switchSource, openSwitch);
    }

    public void PlayTakeCoinSound()
    {
        PlaySound(powerUp_Coin_HeartSource, coin);        
    }

    public void PlayTakePowerUpSound()
    {
        PlaySound(powerUp_Coin_HeartSource, powerUp);
    }

    public void PlayTakeHeartSound()
    {
        PlaySound(powerUp_Coin_HeartSource, heart);
    }


    //Methods volume
    public void SetMusicVolume(float volume)
    {
        audiomixer.SetFloat("soundTrackVol", volume);
    }    

    public void SetSfxVolume(float volume)
    {
        audiomixer.SetFloat("sfxVol", volume);
    }

    public void MuteAll(bool isMute)
    {
        audiomixer.SetFloat("masterVol", isMute ? 0 : -80);
    }

    public void SnapShotTransition(bool flag)
    {
        if (flag)
        {
            paused.TransitionTo(0.01f);
        }
        else
        {
            normal.TransitionTo(0.01f);
        }
    }

}
