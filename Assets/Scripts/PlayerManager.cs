using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] players;

    void Start()
    {
        int id = GameManager.Instance.selectedCharacter;

        // Desactivar todos
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetActive(false);
        }

        // Activar el elegido
        players[id].SetActive(true);
    }
}
