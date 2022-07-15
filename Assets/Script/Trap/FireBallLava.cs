using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallLava : MonoBehaviour
{
    [SerializeField] Transform pos1, pos2;
    [SerializeField] float speed;
    [SerializeField] float gravityForce = 0.2f;
    
    private ParticleSystem ps;
    private bool gravity = true;

    Vector3 nextPos;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        nextPos = pos1.position;
    }

    private void Update()
    {        
        var main = ps.main;
        
        if (transform.position == pos1.position)
        {            
            gravity = true;
            nextPos = pos2.position;
        }

        if (transform.position == pos2.position)
        {
            gravity = false;                     
            nextPos = pos1.position;
        }

        if(gravity == true)
        {
            main.gravityModifierMultiplier -= gravityForce * Time.deltaTime / 2;
        }
        else
        {
            main.gravityModifierMultiplier += gravityForce * Time.deltaTime / 2;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }   

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }

}
