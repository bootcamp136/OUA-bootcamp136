using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZombieAttack : MonoBehaviour
{
    
    private int damage = 5;
    public EnemyAI _enemyAI;
    ChangeRotateAndShake shakeandRotate;
    StarterAssets.ThirdPersonController tpc;
    HealthBarScript _healthBarScript;


    private void Start()
    {
        shakeandRotate = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ChangeRotateAndShake>();
        tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();
        _healthBarScript = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){

            if (_enemyAI.attack == true)
            {
                //  Vector3 distance = tpc.gameObject.transform.position - gameObject.transform.position;
                //  Debug.Log(distance.magnitude);
                 tpc.playerHealth -= damage;
                _healthBarScript.SetHealth((int)tpc.playerHealth);
                Debug.Log("Current Health Player:" + tpc.playerHealth);
                StartCoroutine(shakeandRotate.ShakeEffect(shakeandRotate.shakeTime));
               
            }
            
            
            //Debug.Log("Kalan Health:"+tpc.playerHealth);
            if (tpc.playerHealth <= 0)
            {
                tpc._animator.SetTrigger("Dead");
                Destroy(tpc.gameObject,7f);
                InputSystem.DisableAllEnabledActions(); //Çalýþtý. Önce kütüphaneyi eklemeliyiz. Yukarýda ekledim. O kýsma dikkat etmekte fayda var.
                // tpc.GetComponent<CharacterController>().enabled = false;
                
                //Tag'ini deðiþtirelim.(Karakterin)
                // tpc.gameObject.tag = "deadPlayer"; //Bunu ekleyebilmemiz için öncelikle tag kýsmýndan editör'e eklememiz gerekir.
                //Böyle olmuyor, enemy hala vurmaya devam ediyor tag'i deðiþtirince de, çünkü tag'i deðiþtirdiðimizi algýlamýyor.

                //Olmadý. Bunun yerine Capsule COllider'ýný pasif hale getirelim .Sonuçta onunla triggerlanýyor.
                //Bu da çalýþmadý.



                //  if (isDestroyingCharacter == true)
                //  {
                //     // Invoke(nameof(tpcAnimatorEnabled), .01f); //Animator'ü kapatýyorum. Böyle olunca direk olarak hiçbir animasyon oynamýyor.
                //      
                //  }
            }
        }
    }
    //Bunda da Exit sürekli çalýþýyor, bu hatayý alýyoruz.
   // private void OnTriggerExit(Collider other)
   // {
   //     Debug.Log("Exit çalýþtý");
   //     if (other.gameObject.CompareTag("Player"))
   //         {
   //        // Invoke(nameof(tpcCapsuleColliderDisabled), .001f);  //Bu da çalýþmadý. Çünkü sadece 1 defa trigger'lanýyor. O da enter olduðunda, bir de exit fonk. yazalým.
   //         DestroyingCharacter();
   //        }
   // }

    private void DestroyingCharacter()
    {
        Destroy(tpc.gameObject, 3.5f);
        //isDestroyingCharacter = true;
    }
   // private void tpcAnimatorEnabled()
   // {
   //     tpc._animator.enabled = false;
   // }

    //private void tpcCapsuleColliderDisabled()
    //{
    //    tpc.gameObject.GetComponent<CapsuleCollider>().enabled = false;
    //}
}
