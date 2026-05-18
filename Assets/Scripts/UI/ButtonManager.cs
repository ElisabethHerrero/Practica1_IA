using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // MAIN MENU

    public void Jugar()
    {
        SceneManager.LoadScene(3);
    }

    public void JugarDirecto()
    {
        SceneManager.LoadScene(1);
    }

    public void ElegirPersonaje()
    {
        SceneManager.LoadScene(2);
    }

    public void Salir()
    {
        Application.Quit();

        // Para probar en el editor
        Debug.Log("Salir del juego");
    }

    // BOTON VOLVER

    public void VolverMenu()
    {
        SceneManager.LoadScene(0);
    }
}