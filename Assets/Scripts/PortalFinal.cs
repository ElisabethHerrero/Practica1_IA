using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalFinal : MonoBehaviour
{
    public int monedasNecesarias = 25;
    public CoinManager coinManager;

    public GameObject[] lucesPortal;

    private bool playerDentro = false;
    private bool portalActivado = false;

    void Start()
    {
        foreach (GameObject luz in lucesPortal)
        {
            luz.SetActive(false);
        }
    }

    void Update()
    {
        if (portalActivado) return;

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
        portalActivado = true;

        foreach (GameObject luz in lucesPortal)
        {
            luz.SetActive(true);
        }

        Debug.Log("Portal final activado.");

        StartCoroutine(CambiarEscenaFinal());
    }

    private IEnumerator CambiarEscenaFinal()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(4);
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