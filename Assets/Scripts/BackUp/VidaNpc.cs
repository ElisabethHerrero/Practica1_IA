using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VidaNpc : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private NPCController npc;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Recibe daño");
        npc = GetComponent<NPCController>();
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            npc.ChangeState(new MorirState(npc));
        }
    }
}
