using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeleccionarPersonaje : MonoBehaviour
{
    [Header("UI")]
    public UnityEngine.UI.Image imagenPersonaje;
    public Sprite[] personajes;

    [Header("OST por personaje")]
    public AudioClip[] musicaPersonajes; // mismo orden que el array de sprites

    [Header("Configuración de audio")]
    [Range(0f, 1f)] public float volumen = 0.8f;
    public float tiempoFade = 0.1f;

    private int indiceActual = 0;
    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volumen;

        ActualizarPersonaje();
    }

    public void Siguiente()
    {
        indiceActual = (indiceActual + 1) % personajes.Length;
        ActualizarPersonaje();
    }

    public void Anterior()
    {
        indiceActual = (indiceActual - 1 + personajes.Length) % personajes.Length;
        ActualizarPersonaje();
    }

    void ActualizarPersonaje()
    {
        // Actualizar imagen
        imagenPersonaje.sprite = personajes[indiceActual];

        // Cambiar música con fade
        if (musicaPersonajes != null && indiceActual < musicaPersonajes.Length)
        {
            AudioClip nuevaMusica = musicaPersonajes[indiceActual];
            if (nuevaMusica != null && audioSource.clip != nuevaMusica)
            {
                if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
                fadeCoroutine = StartCoroutine(FadeHacia(nuevaMusica));
            }
        }
    }

    private IEnumerator FadeHacia(AudioClip nuevoClip)
    {
        // Fade out
        float t = 0f;
        float volInicial = audioSource.volume;
        while (t < tiempoFade)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volInicial, 0f, t / tiempoFade);
            yield return null;
        }

        // Cambiar pista
        audioSource.clip = nuevoClip;
        audioSource.Play();

        // Fade in
        t = 0f;
        while (t < tiempoFade)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volumen, t / tiempoFade);
            yield return null;
        }

        audioSource.volume = volumen;
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