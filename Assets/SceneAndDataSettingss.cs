using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneAndDataSettingss : MonoBehaviour
{
    private int currentScene;
    StarterAssets.ThirdPersonController tpc;
    HealthBarScript _healtBarPlayer;
    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();
        _healtBarPlayer = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBarScript>();
    }

    //OnTriggerStay kullanmak daha ucuz , diðer yerdeki coroutine'i de deðiþtir ve onu da onTriggerStay yap.

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Loadlanabilir Giris Yaptý");
            if (Input.GetKey(KeyCode.L))
            {
                SceneManager.LoadScene(currentScene);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Loadlanabilir");
            if (Input.GetKey(KeyCode.L))
            {
                SceneManager.LoadScene(currentScene);
            }

            if(tpc.playerHealth<=100 && tpc.playerHealth > 0)
            {
                tpc.playerHealth += 10f * Time.deltaTime;
                _healtBarPlayer.GetComponent<RectTransform>().localScale = new Vector3(1, 0.5f, 1);
                Debug.Log(tpc.playerHealth);
            }
        }

        
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Loadlanamaz");
    }


}
