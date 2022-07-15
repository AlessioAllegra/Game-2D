using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour, IKillable
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawn;
    [SerializeField] private GameObject dropInvul;
    SpriteRenderer spriteRenderer;
    private float maxHealth;
    private float health;
    private float tmpHealth;
    [SerializeField] private HUD _HUD;
    private int powerUP;
    Renderer rend;
    Color c;

    private Animator anim;    

    private void Start()
    {
        anim = GetComponent<Animator>();        
        rend = GetComponent<Renderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        c = rend.material.color;
    }

    private void Update()
    {
        Debug.Log("Health Player: " + health);
        Debug.Log("Health MAX: " + maxHealth);
        CheckUseDrop();
    }

    private void Awake()
    {
        maxHealth = PlayerPrefs.GetFloat("PLAYERHEALTH2");
        tmpHealth = PlayerPrefs.GetFloat("PLAYERHEALTH");
        health = tmpHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //11 = Enemy
        if(collision.gameObject.layer == 11)
        {
            Debug.Log("collision enemy");
            HitPlayer(collision);
            if (health >= 1)
            {
                StartCoroutine("GetInvulnerable");
            }                       
        }
        //12 = PowerUp-Heart
        if(collision.gameObject.layer == 12)
        {
            Debug.Log("collision powerup_heart");
            SoundManager.Instance.PlayTakeHeartSound();
            collision.gameObject.SetActive(false);
            HealPlayer(collision);
        }
        //17 = Thorn
        if(collision.gameObject.layer == 17)
        {
            Debug.Log("collision thorn");
            HitPlayer(collision);
            if (health >= 1)
            {
                player.transform.position = respawn.transform.position;
            }                           
        }
        //23 = PowerUp_Invulnerable
        if (collision.gameObject.layer == 23)
        {
            Debug.Log("collision powerup_invul");            
            collision.gameObject.SetActive(false);
            PlayerPrefs.SetInt("BOOLDROP", 1);                      
        }
    }

    private void HealPlayer(Collider2D collision)
    {
        float heal = 1;
        Heal(heal);        
    }

    private void HitPlayer(Collider2D collision)
    {        
        float damage = 1;
        Hurt(damage);        
    }

    public void Kill()
    {              
        GameManager.Instance.TriggerOnPlayerDeath();        
    }

    //Method hurt player
    public void Hurt(float damage)
    {
        Debug.Log("hit");
        health -= damage;
        SaveGame.SaveHeart(health);
        _HUD.UpdateShowHeart(health);
        
        if (health <= 0) //Player dead
        {
            gameObject.GetComponent<PlayerController2D>().enabled = false;
            gameObject.GetComponent<PlayerCombatController2D>().enabled = false;
            Physics2D.IgnoreLayerCollision(8, 11, true);
            Physics2D.IgnoreLayerCollision(8, 17, true);
            anim.SetTrigger("isDeath");
            Debug.Log("dead");
            SoundManager.Instance.PlayRandomPlayerDeathSound();
            Kill();                       
        }

        if(health >= 1) //Player hit
        {
            anim.SetTrigger("Hurt");
            SoundManager.Instance.PlayRandomPlayerHurtSound();
        }        
    }

    //Method heal player
    public void Heal(float heal)
    {
        health += heal;        
        
        if (health > maxHealth)
        {
            health = maxHealth;
            SaveGame.SaveHeart(health);
            _HUD.UpdateShowHeart(health);
        }
        else
        {
            SaveGame.SaveHeart(health);
            _HUD.UpdateShowHeart(health);
        }
        Debug.Log("healing");
    }

    //Invulnerability after hit
    IEnumerator GetInvulnerable()
    {
        Debug.Log("player invulnerable");
        Physics2D.IgnoreLayerCollision(8, 11, true);
        c.a = 0.7f;
        rend.material.color = c;
        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreLayerCollision(8, 11, false);
        c.a = 1f;
        rend.material.color = c;
    }

    //Drop Invulnerability
    private void CheckUseDrop()
    {
        if (PlayerPrefs.GetInt("BOOLDROP")==1 && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine("GetInvulnerableDrop");
        }

        if(PlayerPrefs.GetInt("BOOLDROP") == 1)
        {
            dropInvul.SetActive(true);
        }
    }

    //Invulnerability after use drop
    IEnumerator GetInvulnerableDrop()
    {
        Debug.Log("player invulnerable drop");        
        Physics2D.IgnoreLayerCollision(8, 11, true);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(4f);
        Physics2D.IgnoreLayerCollision(8, 11, false);
        spriteRenderer.color = Color.white;
        dropInvul.SetActive(false);
        PlayerPrefs.SetInt("BOOLDROP", 0);
    }

}
