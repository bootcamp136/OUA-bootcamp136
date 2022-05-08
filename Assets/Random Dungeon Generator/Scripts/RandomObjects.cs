using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjects : MonoBehaviour
{
    public GameObject[] Objeler;
    private int randomNumber;

    private void Start()
    {
        Invoke("SpawnObject", .3f);
    }
    private void SpawnObject()
    {
        randomNumber = Random.Range(0, 100);
        Debug.Log("Random Sayýmýz:" + randomNumber);

        if(0<randomNumber && randomNumber < 20)
        {
            Instantiate(Objeler[0], this.gameObject.transform.position, Quaternion.identity);
        }

        else if (20 < randomNumber && randomNumber < 40)
        {
            Instantiate(Objeler[1], this.gameObject.transform.position, Quaternion.identity);
        }
        else if (40 < randomNumber && randomNumber < 60)
        {
            Instantiate(Objeler[2], this.gameObject.transform.position, Quaternion.identity);
        }
        else if (60 < randomNumber && randomNumber < 80)
        {
            Instantiate(Objeler[3], this.gameObject.transform.position, Quaternion.identity);
        }
        else if (80 < randomNumber && randomNumber < 100)
        {
            Instantiate(Objeler[4], this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
