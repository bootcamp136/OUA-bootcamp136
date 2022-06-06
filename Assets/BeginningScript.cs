using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeginningScript : MonoBehaviour
{
    StarterAssets.ThirdPersonController tpc;
    private Animator _animator;

    
    private void Start()
    {
        tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();
        _animator = GetComponent<Animator>();
      

    }
    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.S) || (IntDegerSceneLoadlama.sceneLoadlanmaSayisi>1 && Input.GetKeyDown(KeyCode.L)))
        {
           // transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Startlandý");
            //Loadlanma ekraný oynasýn Ekran yaklaþýk 5 ya da 6 saniye sürsün. Sonrasýnda tpc enabled olsun.
            
            _animator.SetTrigger("start"); //anim.play'i dene.
            Invoke(nameof(CanvasKapatma),8.5f);
            Invoke(nameof(TpcActive), 8.1f);
            
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            // transform.GetChild(0).gameObject.SetActive(false);
            Debug.Log("Startlandý");
            //Loadlanma ekraný oynasýn Ekran yaklaþýk 5 ya da 6 saniye sürsün. Sonrasýnda tpc enabled olsun.
            Invoke(nameof(AnaEkranPasif),.1f);
            ControlsEkranAktif();
        }
        if (transform.GetChild(3).gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            AnaEkranAktif();
            ControlsEkranPasif();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    private void CanvasKapatma()
    {
        this.gameObject.SetActive(false);
    }
    private void TpcActive()
    {
        tpc.enabled = true;
    }
    private void AnaEkranPasif()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(2).transform.GetChild(3).gameObject.SetActive(false);


    }
    private void AnaEkranAktif()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(2).transform.GetChild(3).gameObject.SetActive(true);
        
    }

    private void ControlsEkranAktif()
    {
        transform.GetChild(3).gameObject.SetActive(true);
    }
    private void ControlsEkranPasif()
    {
        transform.GetChild(3).gameObject.SetActive(false);
    }

}
