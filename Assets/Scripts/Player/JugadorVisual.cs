using UnityEngine;

public class JugadorVisual : MonoBehaviour
{
    public GameObject[] personajes;

    void Start()
    {
        int id = GameManager.Instance.selectedCharacter;

        for (int i = 0; i < personajes.Length; i++)
        {
            personajes[i].SetActive(false);
        }

        personajes[GameManager.Instance.selectedCharacter].SetActive(true);
    }
}