using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Resolutions : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownMenu = default;
    [SerializeField] private Toggle isFullscreen = default;
    private Resolution[] myResolutions;
    void Start()
    {
        myResolutions = Screen.resolutions;
        dropdownMenu.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < myResolutions.Length; i++)
        {
            //string option = myResolutions[i].width + " X " + myResolutions[i].height;
            string option = myResolutions[i].ToString(); //questa opzione mostra anche il RefreshRate
            options.Add(option);
            if (myResolutions[i].width == Screen.width && myResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        dropdownMenu.AddOptions(options);
        dropdownMenu.value = currentResolutionIndex;
        dropdownMenu.RefreshShownValue();
    }

    public void ChangeResolution(int index)
    {
        Screen.SetResolution(myResolutions[index].width, myResolutions[index].height, isFullscreen.isOn);
    }
}
