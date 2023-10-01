using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//因为还没写随机生成算法所以先手动输入数据
public class Room : MonoBehaviour
{
    public Transform[] doors;
    public bool isExplored;
    public int roomNum;
    public Room[] nextRooms;
    public virtual void Initialization()
    {
        isExplored = false;
        OpenDoor();
    }
    
    public virtual void UpdateRoomInfo()
    {

    }
    public virtual void MonsterDie(monstercontrol monster)
    {

    }
    
    public virtual void PlayerEnter()
    {
        GameManager.instance.UpdatePlayerRoom(this);
    }
    public void OpenDoor()
    {
        foreach (Transform door in doors)
        {
            foreach (Animator animator in door.GetComponentsInChildren<Animator>())
            {
                animator.Play("Open");
            }
            
            door.GetComponentInChildren<Collider2D>(true).gameObject.SetActive(false);
        }
    }
    public void CloseDoor()
    {
        foreach (Transform door in doors)
        {
            foreach (Animator animator in door.GetComponentsInChildren<Animator>())
            {
                animator.Play("Close");
            }
            
            door.GetComponentInChildren<Collider2D>(true).gameObject.SetActive(true);
        }
    }
}
