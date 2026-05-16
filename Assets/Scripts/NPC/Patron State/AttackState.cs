using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(NPCController npc) : base(npc) { }

    private float cooldown = 0.8f;
    private float lastAttack;

    public override void Enter()
    {
        Debug.Log("Voy a atacar");
        
    }

    public override void Update()
    {
        if (Time.time >= lastAttack + cooldown)
        {
            npc.attackController.Attack();

            lastAttack = Time.time;
        }

        if (npc.DistanceToPlayer() > 10f)
        {
            npc.ChangeState(new ChaseState(npc));
        }

       
    }
    
    public override void Exit() { }
}
