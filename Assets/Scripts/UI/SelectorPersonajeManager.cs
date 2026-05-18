using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    [Header("UI")]
    public Image imagenPersonaje;

    [Header("Personajes")]
    public Sprite[] personajes;

    [Header("Musica por personaje")]
    public AudioClip[] musicaPersonajes;

    [Header("Audio")]
    public float volumen = 0.8f;

    private int indiceActual = 0;
    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true;
        audioSource.volume = volumen;

        ActualizarPersonaje();
    }

    public void Siguiente()
    {
        if (personajes == null || personajes.Length == 0) return;

        indiceActual = (indiceActual + 1) % personajes.Length;

        Debug.Log("Personaje mostrado: " + indiceActual);

        ActualizarPersonaje();
    }

    public void Anterior()
    {
        if (personajes == null || personajes.Length == 0) return;

        indiceActual = (indiceActual - 1 + personajes.Length) % personajes.Length;

        Debug.Log("Personaje mostrado: " + indiceActual);

        ActualizarPersonaje();
    }

    void ActualizarPersonaje()
    {
        if (personajes == null || personajes.Length == 0)
        {
            Debug.LogWarning("No hay sprites de personajes asignados.");
            return;
        }

        if (imagenPersonaje == null)
        {
            Debug.LogWarning("No hay imagenPersonaje asignada.");
            return;
        }

        imagenPersonaje.sprite = personajes[indiceActual];

        if (musicaPersonajes != null && indiceActual < musicaPersonajes.Length)
        {
            AudioClip nuevaMusica = musicaPersonajes[indiceActual];

            if (nuevaMusica != null && audioSource.clip != nuevaMusica)
            {
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }

                fadeCoroutine = StartCoroutine(CambiarMusica(nuevaMusica));
            }
        }
    }

    System.Collections.IEnumerator CambiarMusica(AudioClip clip)
    {
        float t = 0f;
        float startVolume = audioSource.volume;

        while (t < 0.3f)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / 0.3f);
            yield return null;
        }

        audioSource.clip = clip;
        audioSource.Play();

        t = 0f;

        while (t < 0.3f)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volumen, t / 0.3f);
            yield return null;
        }

        audioSource.volume = volumen;
    }

    public void Aceptar()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("No existe GameManager en la escena.");
            return;
        }

        GameManager.Instance.selectedCharacter = indiceActual;

        if (musicaPersonajes != null && indiceActual < musicaPersonajes.Length)
        {
            GameManager.Instance.selectedMusic = musicaPersonajes[indiceActual];
        }

        // Lo guardamos también en PlayerPrefs por seguridad
        PlayerPrefs.SetInt("PersonajeSeleccionado", indiceActual);
        PlayerPrefs.Save();

        Debug.Log("Personaje guardado en GameManager: " + indiceActual);
        Debug.Log("Personaje guardado en PlayerPrefs: " + indiceActual);

        SceneManager.LoadScene("MainMenu");
    }
}