using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    protected float attack;
    protected string role;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.transform.tag != role && collision.transform.tag != "Bullet"&& collision.transform.tag != "Room"&& collision.transform.tag != "Weapon")
        if (collision.transform.tag != role)
        {
            if (collision.GetComponent<BeAttack>() != null)
            {
                collision.GetComponent<BeAttack>().BeAttack(attack);
                Die();
            }
            else if (collision.transform.tag == "Wall")
            {
                Die();
            }
        }

    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag != role)
    //    {
    //        if (collision.transform.GetComponent<BeAttack>() != null)
    //        {
    //            collision.transform.GetComponent<BeAttack>().BeAttack(attack);
    //            Die();
    //        }
    //        else if (collision.transform.tag == "Wall")
    //        {
    //            Die();
    //        }
    //    }
    //}
    public virtual void Initialization(float attack, string role, float bulletForce)
    {

    }
    private void Die()
    {
        if (hitEffect != null)
        {
            GameObject go = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(go, 0.1f);
        }
        Destroy(gameObject);
    }
}
