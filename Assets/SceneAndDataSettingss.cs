using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneAndDataSettingss : MonoBehaviour
{
    private int currentScene;
   [SerializeField] StarterAssets.ThirdPersonController tpc;
    HealthBarScript _healtBarPlayer;
    float _numDiamondS;

    Animator _animator;

    bool oynadiMi = false;
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        _healtBarPlayer = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();

        _animator = GetComponent<Animator>();


    }

    //OnTriggerStay kullanmak daha ucuz , diðer yerdeki coroutine'i de deðiþtir ve onu da onTriggerStay yap.

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _numDiamondS =StarterAssets.ThirdPersonController._numDiamond; 
            Debug.Log("Loadlanabilir Giris Yaptý");
            if(StarterAssets.ThirdPersonController._numDiamond < 10)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            
         
            if (Input.GetKey(KeyCode.L) && oynadiMi==false &&StarterAssets.ThirdPersonController._numDiamond<10)
            {
                SceneLoadlama();
               
            }
            _healtBarPlayer.GetComponent<RectTransform>().localScale = new Vector3(1, 0.5f, 1);

        }
    }
   

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _healtBarPlayer.GetComponent<RectTransform>().localScale = new Vector3(1, 0.5f, 1);
            Debug.Log("Karakterin Caný:" + tpc.playerHealth); //Seen loadlanýnca karakterin caný fulleniyor.
           
            if (Input.GetKey(KeyCode.L) && oynadiMi == false && StarterAssets.ThirdPersonController._numDiamond < 10)
            {
                StarterAssets.ThirdPersonController._numDiamond = _numDiamondS;
                SceneLoadlama();

            }
           
            if(tpc.playerHealth<=75 && tpc.playerHealth > 0)
            {
                tpc.playerHealth += 10f * Time.deltaTime;
                _healtBarPlayer.GetComponent<HealthBarScript>().SetHealth((int)tpc.playerHealth);
                _healtBarPlayer.GetComponent<RectTransform>().localScale = new Vector3(1, 0.5f, 1);
                Debug.Log(tpc.playerHealth);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log("Loadlanamaz");
       //_healtBarPlayer.GetComponent<RectTransform>().localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }
  
    private void AnimatorAcma()
    {
        _animator.enabled = true;
    }
    private void CanvasAcma()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void SceneLoadlama()
    {
        SceneManager.LoadScene(currentScene);
    }

    

    IEnumerator EightpointEightMinuteCoroutine()
    {
        oynadiMi = true;
        yield return new WaitForSeconds(.01f);
        oynadiMi = false;
        yield return new WaitForSeconds(8.8f);
        oynadiMi = true;
    }
  
}
