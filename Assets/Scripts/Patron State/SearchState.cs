using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : State
{

    //Variables de info
    private Vector3 lastKnownPosition;
    private float searchTime = 3f;
    private float timer;

    public SearchState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        //Esto mira la info que tenemos del jugador y se dirige a esa posición
        lastKnownPosition = npc.player.position;
        npc.agent.destination = lastKnownPosition;
        timer = searchTime;
    }

    public override void Update()
    {
        // Si vuelve a ver al jugador lo persigue
        if (npc.DistanceToPlayer() < npc.detectionRange)
        {
            npc.ChangeState(new ChaseState(npc));
            return;
        }

        // Espera buscando
        if (!npc.agent.pathPending && npc.agent.remainingDistance < 0.5f)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                npc.ChangeState(new PatrolState(npc));
            }
        }
    }

    public override void Exit() { }
}
