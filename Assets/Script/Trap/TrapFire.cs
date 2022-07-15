using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    [SerializeField] private GameObject trapFire, smallFire;
    [SerializeField] private float fireCoolDown = 5;
    private bool fire = true;
    private float lastFire;

    private void Update()
    {
        if (Time.time >= (lastFire + fireCoolDown))
        {
            AttemptToFire();
        }

        CheckFire();
    }
    
    private void AttemptToFire()
    {
        fire = !fire;        
        lastFire = Time.time;
        fireCoolDown = Random.Range(1f, 3f);
    }

    private void CheckFire()
    {
        if(fire)
        {
            trapFire.SetActive(true);
            smallFire.SetActive(false);            
        }
        else
        {
            trapFire.SetActive(false);
            smallFire.SetActive(true);
        }
    }

}
