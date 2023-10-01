using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    public GameObject doorCollider;
   
    // Start is called before the first frame update
    public void Close()
    {
        foreach (Animator animator in GetComponentsInChildren<Animator>())
        {
            animator.Play("Close");
            doorCollider.SetActive(true);
        }
    }
    public void Open()
    {
        foreach (Animator animator in GetComponentsInChildren<Animator>())
        {
            animator.Play("Open");
            doorCollider.SetActive(false);
        }
    }
}
