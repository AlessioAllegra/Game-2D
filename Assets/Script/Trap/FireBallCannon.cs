using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallCannon : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * speed;
        Destroy(gameObject, 4f);
    }

}
