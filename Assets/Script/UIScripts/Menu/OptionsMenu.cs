using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : Menu
{
    [SerializeField] private Slider music = default, sound = default;
    [SerializeField] private Toggle isHudActive = default;
    [SerializeField] private Toggle arrows = default;
    [SerializeField] private Toggle isAudioOn = default;

    private void Awake()
    {
        HideCanvasGroup();
        music.value = SaveGame.GetMusic();
        sound.value = SaveGame.GetSound();
        isAudioOn.isOn = SaveGame.GetAudioIsOn();
        isHudActive.isOn = SaveGame.GetHudIsOn();
        arrows.isOn = SaveGame.GetUseArrows();
    }

    public void SaveOptions()
    {
        SaveGame.SaveMusicVolume(music.value);
        SaveGame.SaveSoundVolume(sound.value);
        SaveGame.SaveAudioIsOn(isAudioOn.isOn);
        SaveGame.SaveHudIsOn(isHudActive.isOn);
        SaveGame.SaveUseArrows(arrows.isOn);
    }
}
