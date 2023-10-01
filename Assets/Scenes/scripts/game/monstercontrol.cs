using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Tracking,
    Stroll,
    Attack,
    Die
}

public class monstercontrol : MonoBehaviour,BeAttack
{

    public float hp;
    public int coin;
    public int magic;
    protected string role = "Monster";
    public bool isStart = false;
    protected Transform targetPosition;
    protected Room room;
    protected MonsterState monsterState;

    public virtual void BeAttack(float data)
    {
        hp -= data;
        if (hp <= 0)
        {
            monsterState = MonsterState.Die;
            GetComponent<Animator>().SetBool("die", true);
            GetComponent<Collider2D>().enabled = false;
            room.MonsterDie(this);
            for (int i = 0; i < coin; i++)
            {
                Instantiate(GameManager.instance.coinPre, transform.position, Quaternion.identity);
            }
            for (int i = 0; i < magic; i++)
            {
                Instantiate(GameManager.instance.mpPre, transform.position, Quaternion.identity);
            }
        }
        else
        {
            GetComponent<Animator>().Play("BeAttack");
            GameManager.instance.ShowAttack(data, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0)));
        }
    }

    void Die()
    {
        Destroy(gameObject);

    }
    public virtual void Initialization(Room room)
    {
        this.room = room;
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
