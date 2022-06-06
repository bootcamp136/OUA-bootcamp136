using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BossSceneGecis : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(DigerSceneYukleme), 1f);
            
            transform.GetChild(1).gameObject.SetActive(true);

            Invoke(nameof(InputKapatma), .2f);

            
        }
    }

    private void DigerSceneYukleme()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void InputKapatma()
    {
        InputSystem.DisableAllEnabledActions();
    }
}
