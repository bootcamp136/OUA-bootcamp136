using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntDegerSceneLoadlama : MonoBehaviour
{
    public static int sceneLoadlanmaSayisi=0;
    [SerializeField] GameObject StartCanvas;
    StarterAssets.ThirdPersonController tpc;
    [SerializeField]GameObject Lekrani;
    [SerializeField] GameObject GirisAcilisiAnimasyon;

    private Scene thisScene;
    private string sceneName;

    
    private void Awake()
    {
        if (sceneLoadlanmaSayisi > 1)
        {
            LekraniAcma();  //L'ye bastýðýmýzda açýlacak olan ekran.
            Invoke(nameof(LekraniKapama), 8.5f);
            Invoke(nameof(tpcAcma), 8.1f);
        }
        thisScene = SceneManager.GetActiveScene();
        sceneName = thisScene.name;


        sceneLoadlanmaSayisi++;
        tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();
      
    }
    void Start()
    {
        Debug.Log(sceneLoadlanmaSayisi);
        if (sceneLoadlanmaSayisi == 1 && sceneName== "Playground 1")
        {
            StartCanvas.SetActive(true);
            GirisAcilisiAnimasyon.SetActive(true);
            Invoke(nameof(GirisEkraniAnimasyonKapatma), 2.1f); 
        }

      

     
    }
    private void LekraniAcma()
    {
        Lekrani.gameObject.SetActive(true);
    }
    private void LekraniKapama()
    {
        Lekrani.gameObject.SetActive(false);
    }
    private void tpcAcma()
    {
        tpc.enabled = true;
    }
    private void TpcKapama()
    {
        tpc.enabled = false;
    }
    private void GirisEkraniAnimasyonAcma()
    {
        GirisAcilisiAnimasyon.SetActive(true);
    }
    private void GirisEkraniAnimasyonKapatma()
    {
        GirisAcilisiAnimasyon.SetActive(false);
    }




}
