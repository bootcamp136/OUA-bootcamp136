using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDamage : MonoBehaviour
{
    public bool getHit;
    private Animator _animator;
    private int swordDamage = 25;
    private int enemyHealth;
    GameObject Baphomet;
    public bool isDead;


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
          
                Debug.Log("Enter �al��t�");
                getHit = true;
                _animator.SetBool("GetHit", getHit);
                enemyHealth -= swordDamage;
            _healthBarScriptEnemy.SetHealth(enemyHealth);
                Debug.Log("Enemy Can�: " + enemyHealth);
                Invoke(nameof(CapsuleColliderKapatma),.01f);
            //  getHit = true;
            //  _animator.SetBool("GetHit", getHit);
            //
            //  enemyHealth -= swordDamage;
            //  Debug.Log("Enemy Can�: "+ enemyHealth);

            if (enemyHealth <= 0)
            {
                isDead = true;
                _animator.SetBool("isDead", isDead);
                gameObject.GetComponent<BoxCollider>().enabled = false; //Karaktere �arp�p kameran�n rotasyonunu de�i�tirmesin diye bunu yapt�k.
                Destroy(this.gameObject, 2.2f);         
            }    
        }
        Invoke(nameof(HitFalselama), .5f); // ** bunu artt�rabiliriz, e�er d�zelmezse buraya bakal�m  
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
}
