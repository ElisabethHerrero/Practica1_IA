using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinManager coinManager;
    public LayerMask playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            if (coinManager != null)
            {
                coinManager.AddCoin();
            }

            Destroy(gameObject);
        }
    }
}