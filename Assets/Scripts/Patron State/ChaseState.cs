using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(NPCController npc) : base(npc) { }

    public override void Enter()
    {
        //deja de patrullar y empieza a perseguir
        npc.agent.isStopped = false;
    }

    public override void Update()
    {
        npc.agent.destination = npc.player.position;

        // Si pierde al jugador lo buscar
        if (npc.DistanceToPlayer() > npc.loseRange)
        {
            npc.ChangeState(new SearchState(npc));
        }

        //

        if (npc.vida == 0)
        {
            npc.ChangeState(new MorirState(npc));
        }
    }

    public override void Exit() { }
}
