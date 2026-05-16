using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Image barraVida; // Arrastra aquí la barra del Canvas

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    private NPCController npc;

    void Start()
    {
        currentHealth = maxHealth;

        npc = GetComponent<NPCController>();

        ActualizarBarra();
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;

        Debug.Log(gameObject.name + " recibió daño");

        ActualizarBarra(); // ← actualizar visualmente

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ActualizarBarra()
    {
        if (barraVida != null)
        {
            barraVida.fillAmount =
                (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        if (npc != null)
        {
            npc.ChangeState(new MorirState(npc));
        }
        else
        {
            Destroy(gameObject);
            SceneManager.LoadScene(6);
        }
    }

    public void Initialize(int health)
    {
        maxHealth = health;
        currentHealth = health;

        ActualizarBarra();
    }
}