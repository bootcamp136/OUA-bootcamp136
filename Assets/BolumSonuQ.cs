using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolumSonuQ : MonoBehaviour
{
  

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }
}
