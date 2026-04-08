using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    //aui patrula 
    public PatrolState(NPCController npc) : base(npc) { }


    public override void Enter()
    {
        npc.agent.isStopped = false;
        GoToNextPoint();
    }

    public override void Update()
    {
        // Detectar jugador
        if(npc.CheckVision())
        {
            npc.ChangeState(new ChaseState(npc));
            return;
        }

        /*
        if (npc.DistanceToPlayer() < npc.detectionRange)
        {

            npc.ChangeState(new ChaseState(npc));
            return;
        }
        */

        // Si llega al punto, ir al siguiente
        if (!npc.agent.pathPending && npc.agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    public override void Exit() { }

    void GoToNextPoint()
    {
        if (npc.patrolPoints.Length == 0) return;

        npc.agent.destination = npc.patrolPoints[npc.currentPatrolIndex].position;
        npc.currentPatrolIndex = (npc.currentPatrolIndex + 1) % npc.patrolPoints.Length;
    }
}
