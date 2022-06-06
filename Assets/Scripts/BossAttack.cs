using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossAttack : MonoBehaviour
{

    private int damage = 15;
    public BossAI _bossAI;
    ChangeRotateAndShake shakeandRotate;
    StarterAssets.ThirdPersonController tpc;
    HealthBarScript _healthBarScript;

    public float PlayerKalanHealth;


    private void Start()
    {

        tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();
        _healthBarScript = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
        shakeandRotate = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ChangeRotateAndShake>();
        PlayerKalanHealth = tpc.playerHealth;
    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (_bossAI.attack == true || _bossAI.DonerekVurma==true)
            {

                StartCoroutine(shakeandRotate.BossShakeEffect());  //Baþka bir coroutine yaz.
                tpc.playerHealth -= damage;
                _healthBarScript.SetHealth((int)tpc.playerHealth);
                Debug.Log("Current Health Player:" + tpc.playerHealth);

            }
            PlayerKalanHealth = tpc.playerHealth;
            if (tpc.playerHealth <= 0)
            {
                tpc._animator.SetTrigger("Dead");
                Destroy(tpc.gameObject, 7f);
                InputKapatma();
                _bossAI.AttackFalselamaa();
                _bossAI.DonerekVurmaPasif();

                

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
