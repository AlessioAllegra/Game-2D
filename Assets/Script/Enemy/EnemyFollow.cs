using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : EnemyManager
{    
    [SerializeField] private GameObject player;
    [SerializeField] private Transform BatDie;
    private Transform target;
    private Vector2 currentPos;
    [SerializeField] private float distance;
    private Animator anim;
    
    

    void Start()
    {
        anim = GetComponent<Animator>();
        target = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;        
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) < distance)
        {
            anim.SetBool("detectionPlayer", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } 
        else if(!(Vector2.Distance(transform.position, currentPos) <= 0))
        {
            anim.SetBool("detectionPlayer", false);
            transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);
        }

        BatDie.transform.position = transform.position;
        EnemyDestroy(BatDie);

    }

    
}
