using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed = 2;
    public float getDistance = 5;
    Vector3 target;
    Transform player;
    bool isDrop;
    void Start()
    {
        target = transform.position + new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0);
        player = GameManager.instance.troops[0].transform;
    }
    private void Update()
    {
        if (!isDrop)
        {
            if (Vector3.Distance(transform.position, target) > 0.01)
            {
                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
            }
            else
            {
                isDrop = true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, player.position) <= getDistance)
            {
                transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed);
            }
            if (Vector3.Distance(transform.position, player.position) <= 0.5)
            {
                player.GetComponent<playercontrol>().AddCoin();
                Destroy(gameObject);
            }
        }
    }
}
