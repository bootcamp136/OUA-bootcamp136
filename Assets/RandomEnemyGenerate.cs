using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyGenerate : MonoBehaviour
{
    [SerializeField] GameObject Enemy1; //Zombi
    [SerializeField] GameObject Letter1;
    [SerializeField] GameObject Letter2;
    [SerializeField] GameObject Letter3;
    [SerializeField] GameObject Letter4;
    [SerializeField] GameObject Letter5;
    [SerializeField] GameObject Letter6;
    [SerializeField] GameObject Letter7;
    [SerializeField] GameObject Letter8;
    [SerializeField] GameObject Letter9;
    [SerializeField] GameObject Letter10;
    [SerializeField] GameObject Pickable;

    private void Start()
    {
        int randomSayi = Random.Range(0, 100);


        if(0<=randomSayi && randomSayi < 5)
        {
            Invoke(nameof(Letter1Instantiate), 3.2f);
        }
        if (5 <= randomSayi && randomSayi < 10)
        {
            Invoke(nameof(Letter2Instantiate), 3.2f);
        }
        if (10 <= randomSayi && randomSayi <15)
        {
            Invoke(nameof(Letter3Instantiate), 3.2f);
        }
        if (15 <= randomSayi && randomSayi < 20)
        {
            Invoke(nameof(Letter4Instantiate), 3.2f);
        }
        if (20 <= randomSayi && randomSayi < 25)
        {
            Invoke(nameof(Letter5Instantiate), 3.2f);
        }
        if (25 <= randomSayi && randomSayi < 30)
        {
            Invoke(nameof(Letter6Instantiate), 3.2f);
        }
        if (30 <= randomSayi && randomSayi < 35)
        {
            Invoke(nameof(Letter7Instantiate), 3.2f);
        }
        if (35 <= randomSayi && randomSayi < 40)
        {
            Invoke(nameof(Letter8Instantiate), 3.2f);
        }
        if (40 <= randomSayi && randomSayi < 45)
        {
            Invoke(nameof(Letter9Instantiate), 3.2f);

        }
        if (45 <= randomSayi && randomSayi < 50)
        {
            Invoke(nameof(Letter10Instantiate), 3.2f);
        }


        if (50<=randomSayi &&  randomSayi <55)
        {
            Invoke(nameof(Enemy1Instantiate), 3.2f);  //5 çeþit letter olsa ya da 10 tane letter olsa yüzde 5 5 ayýrýp ona göre uygularýz.Þimdilik yüzde 50 letter üretsin.
            Invoke(nameof(Letter10Instantiate), 3.2f);
        }
        if (55 <= randomSayi && randomSayi < 60)
        {
            Invoke(nameof(Enemy1Instantiate), 3.2f);  //5 çeþit letter olsa ya da 10 tane letter olsa yüzde 5 5 ayýrýp ona göre uygularýz.Þimdilik yüzde 50 letter üretsin.
            Invoke(nameof(Letter9Instantiate), 3.2f);
        }
        if (60 <= randomSayi && randomSayi < 60)
        {
            Invoke(nameof(Enemy1Instantiate), 3.2f);  //5 çeþit letter olsa ya da 10 tane letter olsa yüzde 5 5 ayýrýp ona göre uygularýz.Þimdilik yüzde 50 letter üretsin.
            Invoke(nameof(Letter8Instantiate), 3.2f);
        }
        if (65 <= randomSayi && randomSayi < 70)
        {
            Invoke(nameof(Enemy1Instantiate), 3.2f);  //5 çeþit letter olsa ya da 10 tane letter olsa yüzde 5 5 ayýrýp ona göre uygularýz.Þimdilik yüzde 50 letter üretsin.
            Invoke(nameof(Letter7Instantiate), 3.2f);
        }
        if (70<=randomSayi && randomSayi <75)
        {
            Invoke(nameof(Enemy1Instantiate), 3.2f);
            Invoke(nameof(Letter6Instantiate), 3.2f);

        }
        if (75 <= randomSayi && randomSayi < 80)
        {
            Invoke(nameof(Enemy1Instantiate), 3.2f);
            Invoke(nameof(Letter5Instantiate), 3.2f);


        }
        if (80 <= randomSayi && randomSayi < 85)
        {
            Invoke(nameof(Enemy1Instantiate), 3.2f);
            Invoke(nameof(Letter4Instantiate), 3.2f);

        }
        if (85<=randomSayi && randomSayi <90)
        {
            Invoke(nameof(PickableInstantiate), 3.2f);
            Invoke(nameof(Enemy1Instantiate),4.0f);
        }
        if (90 <= randomSayi && randomSayi < 95)
        {
            Invoke(nameof(Enemy1Instantiate), 4.0f);
        }
        if (95 <= randomSayi && randomSayi < 100)
        {
            Invoke(nameof(PickableInstantiate), 3.2f);
            Invoke(nameof(Enemy1Instantiate), 4.0f);
        }

    }

    private void Enemy1Instantiate()
    {
        Instantiate(Enemy1, gameObject.transform.position, Quaternion.identity);
    }
    
   
    private void PickableInstantiate()
    {
        Instantiate(Pickable, gameObject.transform.position + new Vector3(3.2f, 1.3f, 3.2f), Quaternion.Euler(-90,0,0));
    }
    private void Letter1Instantiate()
    {
        Instantiate(Letter1, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }
    private void Letter2Instantiate()
    {
        Instantiate(Letter2, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }
    private void Letter3Instantiate()
    {
        Instantiate(Letter3, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }
    private void Letter4Instantiate()
    {
        Instantiate(Letter4, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }
    private void Letter5Instantiate()
    {
        Instantiate(Letter5, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }
    private void Letter6Instantiate()
    {
        Instantiate(Letter6, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }
    private void Letter7Instantiate()
    {
        Instantiate(Letter7, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }
    private void Letter8Instantiate()
    {
        Instantiate(Letter8, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }
    private void Letter9Instantiate()
    {
        Instantiate(Letter9, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }
    private void Letter10Instantiate()
    {
        Instantiate(Letter10, gameObject.transform.position + new Vector3(3.2f, 1.5f, 3.2f), Quaternion.identity);
    }

}
