using UnityEngine;

public class PlayerPersonajeVisual : MonoBehaviour
{
    public GameObject[] modelosPersonaje;

    public MovimientoPlayerTerceraPersona movimientoPlayer;
    public PlayerControles playerControles;

    void Start()
    {
        int personajeSeleccionado = PlayerPrefs.GetInt("PersonajeSeleccionado", 0);

        for (int i = 0; i < modelosPersonaje.Length; i++)
        {
            bool activo = i == personajeSeleccionado;
            modelosPersonaje[i].SetActive(activo);

            if (activo)
            {
                Animator animatorActivo = modelosPersonaje[i].GetComponentInChildren<Animator>();

                if (movimientoPlayer != null)
                {
                    movimientoPlayer.animator = animatorActivo;
                }

                if (playerControles != null)
                {
                    playerControles.animator = animatorActivo;
                }
            }
        }
    }
}