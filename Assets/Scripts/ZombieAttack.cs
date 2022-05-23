using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZombieAttack : MonoBehaviour
{
    
    private int damage = 1;
    public EnemyAI _enemyAI;
    [SerializeField] ChangeRotateAndShake shakeandRotate;
   
    
   // private bool isDestroyingCharacter;
    public StarterAssets.ThirdPersonController tpc; //Public yapt�m, direk olarak edit�rden veriyorum. ��nk� scriptten ula�maya �al���nca null referance diyor.
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){

            if (_enemyAI.attack == true)
            {
             //  Vector3 distance = tpc.gameObject.transform.position - gameObject.transform.position;
             //  Debug.Log(distance.magnitude);
                tpc.playerHealth -= damage;
                StartCoroutine(shakeandRotate.ShakeEffect(shakeandRotate.shakeTime));
               
            }
            
            
            //Debug.Log("Kalan Health:"+tpc.playerHealth);
            if (tpc.playerHealth <= 0)
            {
                tpc._animator.SetTrigger("Dead");
                Destroy(tpc.gameObject,7f);
                InputSystem.DisableAllEnabledActions(); //�al��t�. �nce k�t�phaneyi eklemeliyiz. Yukar�da ekledim. O k�sma dikkat etmekte fayda var.
                // tpc.GetComponent<CharacterController>().enabled = false;
                
                //Tag'ini de�i�tirelim.(Karakterin)
                // tpc.gameObject.tag = "deadPlayer"; //Bunu ekleyebilmemiz i�in �ncelikle tag k�sm�ndan edit�r'e eklememiz gerekir.
                //B�yle olmuyor, enemy hala vurmaya devam ediyor tag'i de�i�tirince de, ��nk� tag'i de�i�tirdi�imizi alg�lam�yor.

                //Olmad�. Bunun yerine Capsule COllider'�n� pasif hale getirelim .Sonu�ta onunla triggerlan�yor.
                //Bu da �al��mad�.



                //  if (isDestroyingCharacter == true)
                //  {
                //     // Invoke(nameof(tpcAnimatorEnabled), .01f); //Animator'� kapat�yorum. B�yle olunca direk olarak hi�bir animasyon oynam�yor.
                //      
                //  }
            }
        }
    }
    //Bunda da Exit s�rekli �al���yor, bu hatay� al�yoruz.
   // private void OnTriggerExit(Collider other)
   // {
   //     Debug.Log("Exit �al��t�");
   //     if (other.gameObject.CompareTag("Player"))
   //         {
   //        // Invoke(nameof(tpcCapsuleColliderDisabled), .001f);  //Bu da �al��mad�. ��nk� sadece 1 defa trigger'lan�yor. O da enter oldu�unda, bir de exit fonk. yazal�m.
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
