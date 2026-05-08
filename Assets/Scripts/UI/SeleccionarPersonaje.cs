using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeleccionarPersonaje : MonoBehaviour
{
    public UnityEngine.UI.Image imagenPersonaje;
    public Sprite[] personajes;

    private int indiceActual = 0;

    void Start()
    {
        ActualizarImagen();
    }

    public void Siguiente()
    {
        indiceActual++;

        if (indiceActual >= personajes.Length)
            indiceActual = 0;

        ActualizarImagen();
    }

    public void Anterior()
    {
        indiceActual--;

        if (indiceActual < 0)
            indiceActual = personajes.Length - 1;

        ActualizarImagen();
    }

    void ActualizarImagen()
    {
        imagenPersonaje.sprite = personajes[indiceActual];
    }

    public void Jugar()
    {
        SceneManager.LoadScene(2);
    }

    public void ElegirPersonaje()
    {
        SceneManager.LoadScene(3);
    }

    public void Salir()
    {
        Application.Quit();
    }
}