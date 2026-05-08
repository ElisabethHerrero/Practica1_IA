using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeleccionPersonajeUI : MonoBehaviour
{
    public Image imagenPersonaje;
    public Sprite[] spritesPersonajes;

    private int indiceActual = 0;

    void Start()
    {
        indiceActual = PlayerPrefs.GetInt("PersonajeSeleccionado", 0);
        ActualizarImagen();
    }

    public void Siguiente()
    {
        indiceActual++;

        if (indiceActual >= spritesPersonajes.Length)
        {
            indiceActual = 0;
        }

        ActualizarImagen();
    }

    public void Anterior()
    {
        indiceActual--;

        if (indiceActual < 0)
        {
            indiceActual = spritesPersonajes.Length - 1;
        }

        ActualizarImagen();
    }

    public void Aceptar()
    {
        PlayerPrefs.SetInt("PersonajeSeleccionado", indiceActual);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");
    }

    void ActualizarImagen()
    {
        if (imagenPersonaje != null &&
            spritesPersonajes != null &&
            spritesPersonajes.Length > 0)
        {
            imagenPersonaje.sprite = spritesPersonajes[indiceActual];
        }
    }
}