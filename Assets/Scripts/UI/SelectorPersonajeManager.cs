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
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.loop = true;
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
        if (personajes == null || personajes.Length == 0) return;
        if (imagenPersonaje == null) return;

        imagenPersonaje.sprite = personajes[indiceActual];

        // Música por personaje
        if (musicaPersonajes != null && indiceActual < musicaPersonajes.Length)
        {
            AudioClip nuevaMusica = musicaPersonajes[indiceActual];

            if (nuevaMusica != null && audioSource.clip != nuevaMusica)
            {
                if (fadeCoroutine != null)
                    StopCoroutine(fadeCoroutine);

                fadeCoroutine = StartCoroutine(CambiarMusica(nuevaMusica));
            }
        }
    }

    System.Collections.IEnumerator CambiarMusica(AudioClip clip)
    {
        float t = 0f;
        float startVolume = audioSource.volume;

        // Fade out
        while (t < 0.3f)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / 0.3f);
            yield return null;
        }

        audioSource.clip = clip;
        audioSource.Play();

        t = 0f;

        // Fade in
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
        GameManager.Instance.selectedCharacter = indiceActual;
        GameManager.Instance.selectedMusic = musicaPersonajes[indiceActual];

        SceneManager.LoadScene("MainMenu");
    }

}