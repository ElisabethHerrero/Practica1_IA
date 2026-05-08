using UnityEngine;

public class PortalFinal : MonoBehaviour
{
    public int monedasNecesarias = 25;
    public CoinManager coinManager;

    public GameObject[] lucesPortal;

    private bool playerDentro = false;

    void Start()
    {
        foreach (GameObject luz in lucesPortal)
        {
            luz.SetActive(false);
        }
    }

    void Update()
    {
        if (playerDentro && Input.GetKeyDown(KeyCode.R))
        {
            if (coinManager != null && coinManager.coins >= monedasNecesarias)
            {
                ActivarPortal();
            }
            else
            {
                Debug.Log("Necesitas al menos " + monedasNecesarias + " monedas.");
            }
        }
    }

    private void ActivarPortal()
    {
        foreach (GameObject luz in lucesPortal)
        {
            luz.SetActive(true);
        }

        Debug.Log("Portal final activado.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDentro = false;
        }
    }
}