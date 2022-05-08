using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 --> need bottom door   (Top door needs to bottom door)
    //2 --> need top door      (Bottom door needs to top door)
    //3 --> need left door     (Right door needs to left door).
    //4 --> need right door    (Left door needs to right door)

    private RoomTemplates templates;
    private int randomNumber;
    private bool spawned=false;

    //    public float waitTime = 4.0f;

    private void Start()
    {
        //Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f); //0.1 s sonra �a��r�laca�� i�in ilk �nce onTrigger metodu �a��r�l�r.
                               //Invoke ile �a��rmam�z�n as�l sebebi FindGameObjectWithTag diyince biraz bekleme s�resi koymam�z gerekti�indendir diye d���n�yorum.
    }

    private void Spawn()
    {
       if (spawned == false)  //Bu kod olmazsa olu�an script'teki tekrardan olu�acak olan spawn point di�erinin �zerinde instantiate edilir?
       {
            if (openingDirection == 1)
            {
                //Need to spawn a room with a Bottom door.
                randomNumber = Random.Range(0,templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[randomNumber], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 2)
            {
                //Need to spawn a room with a top door.
                randomNumber = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[randomNumber], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 3)
            {
                //Need to spawn a room with a left door.
                randomNumber = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[randomNumber], transform.position, Quaternion.identity);
            }
            else if (openingDirection == 4)
            {
                //Need to spawn a room with a right door.
                randomNumber = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[randomNumber], transform.position, Quaternion.identity);
            }

          spawned = true;
     }

    }
    private void OnTriggerEnter(Collider other)
    { //BU KISIMDA DE����KL�KLER OLAB�L�R.
       if(other.gameObject.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {

                Destroy(gameObject); //? Benim i�in soru i�areti bu k�s�m.
                //Invoke("Spawn", .2f); //Object referance null �eviriyorsa ve compare tag falan diyorsak Invoke kullan.
            }
            //objeyi yok ettikten sonra zaten  true'ya �evirsek de bir �ey farketmez ki.....

            // basit�e spawn'� true olarak ayarlay�n, ��nk� onu yok edersek, ortaya ��kabilecek di�er spawn noktalar�yla
            //�arp��mak i�in orada olmayacak ve sonu� olarak odalar birbirinin �st�nde do�acakt�r.

          // else if (other.GetComponent<RoomSpawner>().spawned == false && spawned == true) // �lk ba�ta ilk olu�an g�rcek, yani bizim other'�m�z sonradan olu�an obje olacak.
          // {
          //     Destroy(other.gameObject);
          // }
            spawned = true;  // �ki tane obje kar��la��nca, spawned'� true yapt� ki ba�ka bir prefab instantiate edilmesin.

       
        }
        
    }
}
