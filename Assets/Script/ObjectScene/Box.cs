using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    [SerializeField] GameObject boxClosed;
    [SerializeField] GameObject boxOpen;
    [SerializeField] GameObject coin;
    

    public void OpenBox()
    {
        boxClosed.SetActive(false);
        boxOpen.SetActive(true);
        coin.SetActive(true);
    }

}
