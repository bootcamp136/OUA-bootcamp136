using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZombieAttack : MonoBehaviour
{
    
    private int damage = 30;
    public EnemyAI _enemyAI;
    ChangeRotateAndShake shakeandRotate;
    StarterAssets.ThirdPersonController tpc;
    HealthBarScript _healthBarScript;

   


   

    private void Start()
    {
        
        tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();
        _healthBarScript = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        shakeandRotate = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ChangeRotateAndShake>();
     
    }

 
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (_enemyAI.attack == true)
            {
                
                StartCoroutine(shakeandRotate.ShakeEffect(shakeandRotate.shakeTime));
                tpc.playerHealth -= damage;
                _healthBarScript.SetHealth((int)tpc.playerHealth);
                Debug.Log("Current Health Player:" + tpc.playerHealth);
              
            }
          
            if (tpc.playerHealth <= 0)
            {
                tpc._animator.SetTrigger("Dead");
                Destroy(tpc.gameObject,7f);
                InputKapatma();
               

            }

        }
    }


    public void InputKapatma()
    {
        InputSystem.DisableAllEnabledActions();
    }

    private void Kapatma()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

   
    
    
    
    
    
    

    
    
    
    
    
    
    
    
    
}
