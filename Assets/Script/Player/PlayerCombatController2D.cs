using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController2D : MonoBehaviour
{
    [SerializeField] private bool combatEnabled;
    [SerializeField] private Transform attackHitBoxPos;
    [SerializeField] private float inputTimer;
    [SerializeField] private LayerMask whatIsEnemies;
    [SerializeField] private LayerMask whatIsBox;
    [SerializeField] private LayerMask whatIsSwitch;
    [SerializeField] private float attackRadius;
    [SerializeField] private int attackDamage = 1;
    private bool gotInput;
    private bool isAttacking;
    private bool isFirstAttack;
    private float lastInputTime = Mathf.NegativeInfinity;      

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();        
    }

    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
        {
            if(combatEnabled)
            {
                //Attempt combat
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if(gotInput)
        {
            //Perform attack1
            if(!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;

                SoundManager.Instance.PlayRandomPlayerAttackSound();

                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }

        if(Time.time >= lastInputTime + inputTimer)
        {
            //wait for new input
            gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, whatIsEnemies);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyManager>().TakeDamage(attackDamage);
            Debug.Log("taken dmg");
        }

        Collider2D[] activeBox = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, whatIsBox);

        for (int i = 0; i < activeBox.Length; i++)
        {
            activeBox[i].GetComponent<Box>().OpenBox();
            //SoundManager.Instance.PlayOpenBox();
            Debug.Log("Open Box");
        }

        Collider2D[] activeSwitch = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, whatIsSwitch);

        for (int i = 0; i < activeSwitch.Length; i++)
        {
            activeSwitch[i].GetComponent<Switch>().ActiveSwitch();
            SoundManager.Instance.PlayOpenSwitch();
            Debug.Log("Open Switch");
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
    }


}
