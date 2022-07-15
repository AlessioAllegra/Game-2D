using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveGame
{
    public static void SaveScore(int score)
    {
        PlayerPrefs.SetInt("SCORE", score);
        PlayerPrefs.Save();
    }

    public static int GetScore()
    {
        return PlayerPrefs.GetInt("SCORE");
    }

    public static void SaveCoin(int coin)
    {
        PlayerPrefs.SetInt("COIN", coin);
        PlayerPrefs.Save();
    }

    public static int GetCoin()
    {
        return PlayerPrefs.GetInt("COIN");
    }

    public static void SaveHeart(float heart)
    {
        PlayerPrefs.SetFloat("PLAYERHEALTH", heart);
        PlayerPrefs.Save();
    }

    public static float GetHeart()
    {
        return PlayerPrefs.GetFloat("PLAYERHEALTH");
    }

    public static void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MUSIC", volume);
        PlayerPrefs.Save();
    }

    public static void SaveSoundVolume(float volume)
    {
        PlayerPrefs.SetFloat("SOUND", volume);
        PlayerPrefs.Save();
    }

    public static void SaveHudIsOn(bool value)
    {
        PlayerPrefs.SetInt("HUD", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void SaveUseArrows(bool value)
    {
        PlayerPrefs.SetInt("ARROWS", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void SaveAudioIsOn(bool value)
    {
        PlayerPrefs.SetInt("AUDIO", value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static float GetMusic()
    {
        return PlayerPrefs.GetFloat("MUSIC");
    }

    public static float GetSound()
    {
        return PlayerPrefs.GetFloat("SOUND");
    }

    public static bool GetHudIsOn()
    {
        return PlayerPrefs.GetInt("HUD") == 1 ? true : false;
    }

    public static bool GetUseArrows()
    {
        return PlayerPrefs.GetInt("ARROWS") == 1 ? true : false;
    }

    public static bool GetAudioIsOn()
    {
        return PlayerPrefs.GetInt("AUDIO") == 1 ? true : false;
    }

}
