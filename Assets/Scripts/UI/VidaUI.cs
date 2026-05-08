using TMPro;
using UnityEngine;

public class VidaUI : MonoBehaviour
{
    public Vida playerVida;
    public TextMeshProUGUI vidaText;

    void Update()
    {
        if (playerVida != null && vidaText != null)
        {
            vidaText.text = "Vida: " + playerVida.CurrentHealth + " %";
        }
    }
}