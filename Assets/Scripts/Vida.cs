using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

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
        }
    }


}
