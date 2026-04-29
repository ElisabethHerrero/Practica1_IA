using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinManager coinManager;
    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            coinManager.AddCoin();
            Destroy(gameObject);
        }
    }
}
