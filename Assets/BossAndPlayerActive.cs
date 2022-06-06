using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAndPlayerActive : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    [SerializeField] GameObject Player;
    void Start()
    {
        Invoke(nameof(BossActive), 1.88f);
        Invoke(nameof(PlayerActive), 1.8f);
    }

    private void BossActive()
    {
        Boss.gameObject.SetActive(true);
    }
    private void PlayerActive()
    {
        Player.gameObject.SetActive(true);
    }

}
