using UnityEngine;

public class PlayerPersonajeVisual : MonoBehaviour
{
    public GameObject[] modelosPersonaje;

    public MovimientoPlayerTerceraPersona movimientoPlayer;
    public PlayerControles playerControles;

    private Animator animatorActivo;

    void Start()
    {
        int personajeSeleccionado = 0;

        if (GameManager.Instance != null)
        {
            personajeSeleccionado = GameManager.Instance.selectedCharacter;
            Debug.Log("Personaje leído desde GameManager: " + personajeSeleccionado);
        }
        else
        {
            Debug.LogWarning("No hay GameManager. Se usará personaje 0.");
        }

        if (personajeSeleccionado < 0 || personajeSeleccionado >= modelosPersonaje.Length)
        {
            Debug.LogWarning("Índice de personaje fuera de rango. Se usará personaje 0.");
            personajeSeleccionado = 0;
        }

        for (int i = 0; i < modelosPersonaje.Length; i++)
        {
            modelosPersonaje[i].SetActive(false);
        }

        GameObject modeloActivo = modelosPersonaje[personajeSeleccionado];
        modeloActivo.SetActive(true);

        animatorActivo = modeloActivo.GetComponent<Animator>();

        if (animatorActivo == null)
        {
            animatorActivo = modeloActivo.GetComponentInChildren<Animator>(true);
        }

        if (animatorActivo == null)
        {
            Debug.LogError("No se encontró Animator en: " + modeloActivo.name);
            return;
        }

        Debug.Log("Modelo activo real: " + modeloActivo.name);
        Debug.Log("Animator activo real: " + animatorActivo.gameObject.name);

        animatorActivo.Rebind();
        animatorActivo.Update(0f);

        if (movimientoPlayer != null)
        {
            movimientoPlayer.animator = animatorActivo;
        }

        if (playerControles != null)
        {
            playerControles.SetAnimator(animatorActivo);
        }
    }

    public Animator GetAnimatorActivo()
    {
        return animatorActivo;
    }
}