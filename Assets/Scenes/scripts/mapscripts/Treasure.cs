using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject treasurePre;
    public Transform treasureTran;
    bool isOpen = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && !isOpen)
        {
            Instantiate(treasurePre, treasureTran);
            GetComponent<Animator>().Play("Open");
            isOpen = true;
        }
    }
}
