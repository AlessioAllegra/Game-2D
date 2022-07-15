using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject platformPrefab;
    [SerializeField] Transform positionPlatform;    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {            
            Invoke("DropPlatform", 0.5f);            
            Destroy(gameObject, 4.1f);
            StartCoroutine(SpawnPlatform());
        }
    }

    private void DropPlatform()
    {
        rb.isKinematic = false;
    }

    IEnumerator SpawnPlatform()
    {
        Debug.Log("spa");
        yield return new WaitForSecondsRealtime(4);
        Debug.Log("2s");
        rb.isKinematic = true;
        Instantiate(platformPrefab, positionPlatform.position, positionPlatform.rotation);
        Debug.Log("wn");        
    }
}
