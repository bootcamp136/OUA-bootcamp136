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

    [SerializeField] GameObject portal;

    [SerializeField] GameObject quad;
    public Vector3 lastRoomPosition;
    int i = 0;
    private void Start()
    {
     

        Invoke(nameof(getLastRoomPosition), 3.5f);

        Invoke(nameof(PortalInstantiate), 3.1f);

       

    }

    private void Update()
    {
        if (StarterAssets.ThirdPersonController._numDiamond == 10 && i==0)
        {
            Invoke(nameof(QuadOlusturma), 3.2f);
            i++;
        }
    }
    private void PortalInstantiate() //En son odada Boss'u insantiate ettim.
  {
      Instantiate(portal, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
      
  }

    private void getLastRoomPosition()
    {
        lastRoomPosition = rooms[rooms.Count - 1].transform.position;
    }
    private void QuadOlusturma()
    {
        Instantiate(quad,new Vector3(lastRoomPosition.x,lastRoomPosition.y+2.5f,lastRoomPosition.z) , Quaternion.identity);
    }

}
