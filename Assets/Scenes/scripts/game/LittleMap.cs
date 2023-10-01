using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleMap : MonoBehaviour
{
    public Transform littlemap;
    public Transform playerIndex;
    List<Transform> mapItems;
    void Start()
    {
        mapItems = new List<Transform>();
        foreach (Transform item in littlemap)
        {
            mapItems.Add(item);
        }
        for (int i = 0; i < mapItems.Count; i++)
        {
            mapItems[i].gameObject.SetActive(false);
        }
        mapItems[40].gameObject.SetActive(true);
    }
    /// <summary>
    /// 更新玩家所在的房间
    /// </summary>
    public void UpdatePlayerIndex(int num)
    {
        int mapNum = num / 5 * 18 + num % 5 * 2;
        playerIndex.transform.position = mapItems[mapNum].transform.position;
    }
    /// <summary>
    /// 更新可以探索的地方
    /// </summary>
    public void UpdateCanExploreInMap(Room room)
    {
        foreach (Room nextRoom in room.nextRooms)
        {
            int data = nextRoom.roomNum - room.roomNum;
            int roomNum = room.roomNum;
            int roadNum = 0;
            switch (data)
            {
                case -5://上
                    roadNum = roomNum / 5 * 18 + roomNum % 5 * 2 - 9;
                    break;
                case 5://下
                    roadNum = roomNum / 5 * 18 + roomNum % 5 * 2 + 9;
                    break;
                case -1://左
                    roadNum = roomNum / 5 * 18 + roomNum % 5 * 2 - 1;
                    break;
                case 1://右
                    roadNum = roomNum / 5 * 18 + roomNum % 5 * 2 + 1;
                    break;
                default:
                    break;
            }
            int nextRoomNum = nextRoom.roomNum / 5 * 18 + nextRoom.roomNum % 5 * 2;
            mapItems[nextRoomNum].gameObject.SetActive(true);
            mapItems[roadNum].gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// 更新已探索的房间
    /// </summary>
    public void UpdateRoomsExploredInMap(int room)
    {
        int mapNum = room / 5 * 18 + room % 5 * 2;
        mapItems[mapNum].GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }
    /// <summary>
    /// 显示特定某个房间
    /// </summary>
    public void ShowTheRoom(int room)
    {
        int mapNum = room / 5 * 18 + room % 5 * 2;
        mapItems[mapNum].gameObject.SetActive(true);
    }
}
