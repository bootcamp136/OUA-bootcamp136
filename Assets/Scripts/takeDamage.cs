using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDamage : MonoBehaviour
{
    public bool getHit;
    private Animator _animator;
    private int swordDamage = 50;
    private int enemyHealth;
    GameObject Baphomet;
    public bool isDead;

    [SerializeField] GameObject pickable;
    [SerializeField] GameObject efekt;

    HealthBarScript _healthBarScriptEnemy;

    private void Awake()
    {
        Baphomet = GameObject.FindGameObjectWithTag("baphomet");
      _healthBarScriptEnemy = GameObject.FindGameObjectWithTag("EnemyHealthBar").GetComponent<HealthBarScript>();
        enemyHealth =this.gameObject.GetComponent<EnemyAI>().enemyHealth;
    }
    private void Start()
    {
        
        _animator = GetComponent<Animator>();
        _healthBarScriptEnemy.SetMaxValue(enemyHealth);
    }
    private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.CompareTag("baphomet") && Baphomet.GetComponent<CapsuleCollider>().enabled==true)
        {
          
                Debug.Log("Enter çalýþtý");
                getHit = true;
                _animator.SetBool("GetHit", getHit);
                enemyHealth -= swordDamage;
            _healthBarScriptEnemy.SetHealth(enemyHealth);
                Debug.Log("Enemy Caný: " + enemyHealth);
                Invoke(nameof(CapsuleColliderKapatma),.01f);

            StartCoroutine(EfektAyarlama());

            


            if (enemyHealth <= 0)
            {
                isDead = true;
                _animator.SetBool("isDead", isDead);
                gameObject.GetComponent<BoxCollider>().enabled = false; //Karaktere çarpýp kameranýn rotasyonunu deðiþtirmesin diye bunu yaptýk.
                

                int x = Random.Range(0, 100);
                Debug.Log("Enemy zar sonucu: " + x);
                if (x < 25)
                {
                    Invoke(nameof(PickableInstantiate), 1.2f);
                }
                Invoke(nameof(CloseMesh), 1.0f);
                Destroy(this.gameObject, 2.2f);         
            }    
        }
        Invoke(nameof(HitFalselama), .5f); // ** bunu arttýrabiliriz, eðer düzelmezse buraya bakalým  
    }
    private void CloseMesh()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
    private void PickableInstantiate()
    {
        Instantiate(pickable,new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z),Quaternion.Euler(-90,0,0));
    }


    private void HitFalselama()
    {
        getHit = false;
        _animator.SetBool("GetHit", getHit);
    }


    private void CapsuleColliderKapatma()
    {
        Baphomet.GetComponent<CapsuleCollider>().enabled = false;
    }


    IEnumerator EfektAyarlama()
    {
        yield return new WaitForSeconds(.15f);
        EfektAktif();
        yield return new WaitForSeconds(1f);
        EfektPasif();
    }
    private void EfektAktif()
    {
        efekt.SetActive(true);
    }
    private void EfektPasif()
    {
        efekt.SetActive(false);
    }
}
