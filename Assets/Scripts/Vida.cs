using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    //Propiedad pública para poder consultar la vida actual desde otros scripts (UI, etc.)
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    private NPCController npc;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Solo existirá en NPCs
        npc = GetComponent<NPCController>(); //solo si es el NPC
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;

        Debug.Log(gameObject.name + " recibió daño");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Si es NPC
        if (npc != null)
        {
            npc.ChangeState(new MorirState(npc));
            Debug.Log(gameObject.name + " murió");
        }
        else
        {
            // Player o cualquier otra cosa
            Debug.Log(gameObject.name + " murió");
            Destroy(gameObject);
            SceneManager.LoadScene(6);

        }
    }

    //para los NPCs
    public void Initialize(int health)
    {
        maxHealth = health;
        currentHealth = health;
    }
}