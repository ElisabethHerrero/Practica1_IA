using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorirState : State
{
    private float deathTimer = 2f; //Tiempo que tarda en desaparecer tras morir
    public MorirState(NPCController npc) : base(npc) { }

    //

    private Renderer rend;
    private Color originalColor;
    private float flashSpeed = 10f;

    //Función que se ejecuta al entrar en estado de muerte
    public override void Enter()
    {
        npc.agent.isStopped = true; //Detiene el movimiento del NavMeshAgent (el NPC deja de moverse)

        npc.agent.enabled = false; //Desactiva completamente el NavMeshAgent (ya no calcula rutas)

        Collider collider = npc.GetComponent<Collider>(); //Obtiene el collider del NPC

        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    public override void Update()
    {
        deathTimer -= Time.deltaTime; //Cuenta atrás para que el NPC no desaparezca inmediatamente

        //
        npc.transform.localScale = Vector3.Lerp(
        npc.transform.localScale,
        Vector3.zero,
        Time.deltaTime * 2f
    );

        // Parpadeo rojo
        if (rend != null)
        {
            float t = Mathf.PingPong(Time.time * 10f, 1f);
            rend.material.color = Color.Lerp(originalColor, Color.red, t);
        }

        if (deathTimer <= 0)
        {
            GameObject.Destroy(npc.gameObject);
        }



        //


        if (deathTimer <= 0)
        {
            GameObject.Destroy(npc.gameObject);
        }
    }

    public override void Exit() { }
}
