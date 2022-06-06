using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BossTakeDamage : MonoBehaviour
{
    public bool getHit;
    private Animator _animator;
    private int swordDamage = 30;
     int bossHealth;
    GameObject Baphomet;
    public bool isDead;

    [SerializeField] GameObject pickable;
    [SerializeField] GameObject efekt;
    [SerializeField] GameObject BolumSonuCanvas;
    HealthBarScript _healthBarScriptEnemy;

    [SerializeField] BossAI _bossAI;

    public int kalanHealth;
    private void Awake()
    {
        Baphomet = GameObject.FindGameObjectWithTag("baphomet");
        _healthBarScriptEnemy = GameObject.FindGameObjectWithTag("EnemyHealthBar").GetComponent<HealthBarScript>();
        bossHealth = this.gameObject.GetComponent<BossAI>().BossHealth;
    }
    private void Start()
    {
        kalanHealth = bossHealth;
        _animator = GetComponent<Animator>();
        _healthBarScriptEnemy.SetMaxValue(bossHealth);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("baphomet") && Baphomet.GetComponent<CapsuleCollider>().enabled == true)
        {

            Debug.Log("Enter çalýþtý");
            getHit = true;
            _animator.SetBool("GetHit", getHit);
            bossHealth -= swordDamage;
            
            _healthBarScriptEnemy.SetHealth(bossHealth);
            Debug.Log("Bos Caný: " + bossHealth);
            Invoke(nameof(CapsuleColliderKapatma), .01f);

            StartCoroutine(EfektAyarlama());

            if (bossHealth <= 50)
            {
                _bossAI.attack = false;
                _bossAI.DonerekVurma = true;
            }




            if (bossHealth <= 0) //TODO: ÖLDÜÐÜNDE SWORD'UN COLLÝDER'INI PASÝF HALE GETÝR.
            {
                isDead = true;
                _animator.SetBool("isDead", isDead);
                gameObject.GetComponent<BoxCollider>().enabled = false; //Karaktere çarpýp kameranýn rotasyonunu deðiþtirmesin diye bunu yaptýk.


                int x = Random.Range(0, 100);
                Debug.Log("Enemy zar sonucu: " + x);
               
                Destroy(this.gameObject, 3.0f);

                Invoke(nameof(InputKapatma), 1f);

                Invoke(nameof(BolumSonuCanvasAktif), .5f);
                
            }
            kalanHealth = bossHealth;
        }
        Invoke(nameof(HitFalselama), .5f); // ** bunu arttýrabiliriz, eðer düzelmezse buraya bakalým  
    }
    private void CloseMesh()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
    private void PickableInstantiate()
    {
        Instantiate(pickable, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + .5f, this.gameObject.transform.position.z), Quaternion.identity);
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

    public void InputKapatma()
    {
        InputSystem.DisableAllEnabledActions();
    }
    private void BolumSonuCanvasAktif()
    {
        BolumSonuCanvas.gameObject.SetActive(true);
    }
}
