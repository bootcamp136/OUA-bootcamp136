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
    public GameObject planeBaked;

    private void Start()
    {
       // Invoke("BossInstantiate",2f); // Ne olur ne olmaz Room'larýn oluþma süresine 2 saniye verdim, sonra da arttýrýlabilir.
      //  Invoke(nameof(bakedPlaneInstantiate), 1.5f);
    }

    private void BossInstantiate() //En son odada Boss'u insantiate ettim.
    {
        //Instantiate(boss, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
        
    }
 //   private void bakedPlaneInstantiate()
 //   {
 //       Instantiate(planeBaked, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
 //   }
}
