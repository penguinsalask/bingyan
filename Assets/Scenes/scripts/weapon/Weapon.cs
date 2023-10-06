using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, GetTipsInfo
{
    public float shake;
    public float attack;
    [HideInInspector]
    public string role;
    [HideInInspector]
    public Vector3 target;
    //public int sight;
    public string weaponName;
    public virtual void ShootButtonDown()
    {
        
    }
    public virtual void ShootButtonUp()
    {
        
    }
    public virtual void ShootButtonPressed()
    {
        
    }
    public virtual void UpdateLookAt(Vector3 target)
    {

    }
    public virtual void Initialization(string role,int layer)
    {
        this.role = role;
    }
    
    public void Pickup()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public void PickDown()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public string GetTipsInfo()
    {
        return weaponName;
    }
}
