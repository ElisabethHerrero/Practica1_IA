using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SeleccionarPersonaje : MonoBehaviour
{
    public Image imagenPersonaje;   
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


    //De paso ponemos aqui el resto de botones

    public void Jugar()
    {

        SceneManager.LoadScene(2);
        

    }

    public void Salir()
    {
        
    }




}
