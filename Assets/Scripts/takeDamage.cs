using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDamage : MonoBehaviour
{
    public bool getHit;
    private Animator _animator;
    private int swordDamage = 1;
    public GameObject Enemy;
    private int enemyHealth;
    public GameObject Baphomet;
    public bool isDead;
    
    private void Awake()
    {
         enemyHealth = Enemy.GetComponent<EnemyAI>().enemyHealth;
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
    if (other.gameObject.CompareTag("baphomet") && Baphomet.GetComponent<CapsuleCollider>().enabled==true)
        {
          
                Debug.Log("Enter �al��t�");
                getHit = true;
                _animator.SetBool("GetHit", getHit);
                enemyHealth -= swordDamage;
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
                Destroy(Enemy, 2.2f);         
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
