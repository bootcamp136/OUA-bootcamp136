using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathCanvasYonetimi : MonoBehaviour
{
   
    int thisSceneIndex;

    [SerializeField] TextMeshProUGUI _text;

  

    private void Start()
    {
        thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Invoke(nameof(TextActive), .78f);
        IntDegerSceneLoadlama.sceneLoadlanmaSayisi++;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            IntDegerSceneLoadlama.sceneLoadlanmaSayisi++;
            SceneManager.LoadScene(thisSceneIndex);
            StarterAssets.ThirdPersonController._numDiamond = 0.0f;


        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    private void TextActive()
    {
        _text.gameObject.SetActive(true);
    }

}
