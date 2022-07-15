using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] private GameObject blood;
    public float speed;
    Renderer rend;
    Color c;    

    private void FixedUpdate()
    {
        rend = GetComponent<Renderer>();
        c = rend.material.color;
    }

    public void EnemyDestroy(Transform BatDie)
    {
        if (health <= 0)
        {            
            Destroy(gameObject);
            GameManager.Instance.TriggerOnCoin();
            BatDie.gameObject.SetActive(true);
        }
    }

    public void EnemyDestroy()
    {
        if (health <= 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.Instance.TriggerOnCoin();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        SoundManager.Instance.PlayRandomEnemyHitSound();
        StartCoroutine("ShowHitEnemy");
        Debug.Log("damage taken");
    }

    IEnumerator ShowHitEnemy()
    {
        Debug.Log("Color hit enemy");
        c.a = 2f;
        rend.material.color = c;
        yield return new WaitForSeconds(0.5f);        
        c.a = 1f;
        rend.material.color = c;
    }
}
