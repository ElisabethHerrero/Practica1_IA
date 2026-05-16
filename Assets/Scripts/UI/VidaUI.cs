using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VidaUI : MonoBehaviour
{
    public Vida playerVida;
    public TextMeshProUGUI vidaText;
    public Image pantallaDaño;

    void Update()
    {
        vidaText.text =
            "Vida: " + playerVida.CurrentHealth + "%";

        float vidaNormalizada =
            playerVida.CurrentHealth / 100f;

        float daño =
            1f - vidaNormalizada;

        float alpha =
            Mathf.Pow(daño, 2f) * 0.4f;

        Color c = pantallaDaño.color;
        c.a = alpha;
        pantallaDaño.color = c;
    }
}