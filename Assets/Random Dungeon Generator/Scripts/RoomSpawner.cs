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
        Invoke("Spawn", 0.1f); //0.1 s sonra çaðýrýlacaðý için ilk önce onTrigger metodu çaðýrýlýr.
                               //Invoke ile çaðýrmamýzýn asýl sebebi FindGameObjectWithTag diyince biraz bekleme süresi koymamýz gerektiðindendir diye düþünüyorum.
    }

    private void Spawn()
    {
       if (spawned == false)  //Bu kod olmazsa oluþan script'teki tekrardan oluþacak olan spawn point diðerinin üzerinde instantiate edilir?
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
    { //BU KISIMDA DEÐÝÞÝKLÝKLER OLABÝLÝR.
       if(other.gameObject.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {

                Destroy(gameObject); //? Benim için soru iþareti bu kýsým.
                //Invoke("Spawn", .2f); //Object referance null çeviriyorsa ve compare tag falan diyorsak Invoke kullan.
            }
            //objeyi yok ettikten sonra zaten  true'ya çevirsek de bir þey farketmez ki.....

            // basitçe spawn'ý true olarak ayarlayýn, çünkü onu yok edersek, ortaya çýkabilecek diðer spawn noktalarýyla
            //çarpýþmak için orada olmayacak ve sonuç olarak odalar birbirinin üstünde doðacaktýr.

          // else if (other.GetComponent<RoomSpawner>().spawned == false && spawned == true) // Ýlk baþta ilk oluþan görcek, yani bizim other'ýmýz sonradan oluþan obje olacak.
          // {
          //     Destroy(other.gameObject);
          // }
            spawned = true;  // Ýki tane obje karþýlaþýnca, spawned'ý true yaptý ki baþka bir prefab instantiate edilmesin.

       
        }
        
    }
}
