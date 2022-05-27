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
    public Vector3 lastRoomPosition;
    private void Start()
    {
        Invoke("BossInstantiate",4f); // Ne olur ne olmaz Room'larýn oluþma süresine 2 saniye verdim, sonra da arttýrýlabilir.
        Invoke("BossInstantiateee", 4f); // Ne olur ne olmaz Room'larýn oluþma süresine 2 saniye verdim, sonra da arttýrýlabilir.
        Invoke("BossInstantiateeee", 4f); // Ne olur ne olmaz Room'larýn oluþma süresine 2 saniye verdim, sonra da arttýrýlabilir.
        Invoke(nameof(getLastRoomPosition), 1.5f);
     
    }

    private void BossInstantiate() //En son odada Boss'u insantiate ettim.
    {
        Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        
    }
    private void BossInstantiateee()
    {
        Instantiate(boss, rooms[rooms.Count  -2].transform.position, Quaternion.identity);
    }
    private void BossInstantiateeee()
    {
        Instantiate(boss, rooms[rooms.Count  -3].transform.position, Quaternion.identity);
    }

    private void getLastRoomPosition()
    {
        lastRoomPosition = rooms[rooms.Count - 1].transform.position;
    }

}
