using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject fireBall;
    [SerializeField] float startTimeBetween;
    private float timeBetween;

    private void Start()
    {
        timeBetween = startTimeBetween;
    }

    private void Update()
    {
        if(timeBetween <= 0)
        {
            Instantiate(fireBall, firePoint.position, firePoint.rotation);
            SoundManager.Instance.PlayFireBallSound();
            timeBetween = startTimeBetween;
        }
        else
        {
            timeBetween -= Time.deltaTime;
        }
    }

}
