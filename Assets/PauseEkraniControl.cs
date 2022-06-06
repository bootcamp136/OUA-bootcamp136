using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseEkraniControl : MonoBehaviour
{

    [SerializeField] GameObject _canvasPauseEkrani;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !_canvasPauseEkrani.activeSelf)
        {
            _canvasPauseEkrani.SetActive(true);
            Time.timeScale = 0;
        }

        if(_canvasPauseEkrani.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            _canvasPauseEkrani.SetActive(false);
            Time.timeScale = 1;
        }
        if (_canvasPauseEkrani.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            _canvasPauseEkrani.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
