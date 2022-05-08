using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public List<GameObject> rooms;
    public GameObject boss;

    private void Start()
    {
        Invoke("BossInstantiate",2.0f); // Ne olur ne olmaz Room'lar�n olu�ma s�resine 2 saniye verdim, sonra da artt�r�labilir.
    }

    private void BossInstantiate() //En son odada Boss'u insantiate ettim.
    {
        Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
    }
}
