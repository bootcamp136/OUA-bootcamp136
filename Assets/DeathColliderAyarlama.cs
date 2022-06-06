using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathColliderAyarlama : MonoBehaviour
{
    [SerializeField] StarterAssets.ThirdPersonController tpc;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            tpc.playerHealth = 0;
            Debug.Log("000000000");

        }
    }
    private void TimeAyarlama()
    {
        Time.timeScale = 0;
    }
}
