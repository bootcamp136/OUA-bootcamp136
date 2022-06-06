using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : MonoBehaviour
{
    private bool isInside;

   
    StarterAssets.ThirdPersonController tpc;

    private void Start()
    {
        tpc = GameObject.FindGameObjectWithTag("Player").GetComponent<StarterAssets.ThirdPersonController>();
        
    }

    private void Update()
    {
        if(isInside==true && Input.GetKeyDown(KeyCode.X))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            LetterReadFalselama();
            tpc.enabled = false;    //Script'in enable halini false yapýnca hareket edemiyor.

          //   Cursor.visible = true;
          //   Cursor.lockState = CursorLockMode.None;           Ok'u belirgin hale getirip, sonrasýnda x iþaretiyle mouse yardýmýyla da çýkabiliriz.
        }

        if(isInside == true && Input.GetKeyDown(KeyCode.Escape))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            LetterReadFalselama();
            tpc.enabled = true;

        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInside = true;
            LetterRead();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInside = false;
            LetterReadFalselama();
        }
    }

    private void LetterRead()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
    private void LetterReadFalselama()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void QuitLetter()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
