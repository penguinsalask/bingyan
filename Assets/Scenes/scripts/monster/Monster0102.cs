using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster0102 :monstercontrol
{
    public Transform pos;
    public float attackRange;
    public GameObject bulletPre;
    public int bulletCount;
    public float bulletForce;
    public float CD = 5;
    public int attack;
    float timing;

    void Start()
    {
        timing = -CD;
        monsterState = MonsterState.Idle;
    }
    void Update()
    {
        switch (monsterState)
        {
            case MonsterState.Idle:
                Idle();
                break;
            case MonsterState.Tracking:
                Tracking();
                break;
            case MonsterState.Die:
                Die();
                break;
            default:
                break;
        }
        
    }

    private void Die()
    {
        
    }

    private void Tracking()
    {
        foreach (GameObject item in GameManager.instance.troops)//这里是以不止一个目标为前提写的
        {
            if (Vector2.Distance(item.transform.position, transform.position) < attackRange && Time.time - timing > CD)
            {
                timing = Time.time;
                GetComponent<Animator>().SetTrigger("run");
                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject go = Instantiate(bulletPre, pos.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360)));
                    go.GetComponent<Bullet>().Initialization(attack, role, bulletForce);
                    go.transform.SetParent(GameManager.instance.weaponRecycle);
                }
            }
        }
    }

    private void Idle()
    {
        if (isStart)
        {
            monsterState = MonsterState.Tracking;
        }
    }
}
