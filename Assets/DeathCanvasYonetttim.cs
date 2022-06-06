using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCanvasYonetttim : MonoBehaviour
{
    [SerializeField] GameObject DeathCanvas;
    [SerializeField] StarterAssets.ThirdPersonController tpc;

    private void Update()
    {
        if(tpc.playerHealth <= 0)
        {
            DeathCanvas.SetActive(true);
        }
    }


}
