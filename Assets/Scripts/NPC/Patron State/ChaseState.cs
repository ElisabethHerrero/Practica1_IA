using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        //deja de patrullar y empieza a perseguir
        npc.agent.isStopped = false;
        Debug.Log("Vi al jugador");
    }

    public override void Update()
    {
        /*
        if (npc.player == null)
        {
            npc.ChangeState(new PatrolState(npc));
            return;
        }
        */

        //que se pare sin legar a atravesar al juador
        npc.agent.stoppingDistance = 1f;

        npc.agent.destination = npc.player.position;

        // Si pierde al jugador lo buscar
        if (npc.DistanceToPlayer() > npc.loseRange)
        {
            npc.ChangeState(new SearchState(npc));
            return;
        }

        //Comprobamos que está a la distancia que queremos

        float distance = Vector3.Distance(npc.transform.position, npc.player.position);

        if (distance <= 1f)
        {
            npc.ChangeState(new AttackState(npc));
        }        
    }

    public override void Exit() { }
}
