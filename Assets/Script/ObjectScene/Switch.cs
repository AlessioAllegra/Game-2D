using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    [SerializeField] GameObject switchOpen;
    [SerializeField] GameObject switchClosed;
    [SerializeField] GameObject doorOpen;
    [SerializeField] GameObject doorClosed;


    public void ActiveSwitch()
    {
        switchClosed.SetActive(false);
        switchOpen.SetActive(true);
        doorClosed.SetActive(false);
        doorOpen.SetActive(true);
    }
}
