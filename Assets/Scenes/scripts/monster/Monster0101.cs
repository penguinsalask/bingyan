using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster0101 : monstercontrol
{
    public float strollCD;
    public float attackCD = 0.5f;
    public float speed;
    public int  attack;

    Animator animator;
    monsterAI myAI;
    float strolltiming;
    private void Start()
    {
        myAI = GetComponent<monsterAI>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Stroll:
                Stroll();
                break;
            case MonsterState.Die:
                Die();
                break;
            default:
                break;
        }
    }
    public void UpdateLookAt(Vector2 targetPos)
    {
        if (transform.position.x > targetPos.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x < targetPos.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    private void Die()
    {

    }

    private void Stroll()
    {
        if (Time.time - strolltiming >= strollCD)
        {
            strolltiming = Time.time;
            myAI.RandomPath();
        }
        myAI.NextTarget();
        UpdateLookAt(targetPosition.position);
    }

    private void Idle()
    {
        if (isStart)
        {
            monsterState = MonsterState.Stroll;
            animator.SetBool("run", true);
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<BeAttack>() != null)
        {
            collision.transform.GetComponent<BeAttack>().BeAttack(attack);
        }
    }
}
