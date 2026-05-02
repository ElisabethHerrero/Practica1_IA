using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(NPCController npc) : base(npc) { }

    private float cooldown = 1.5f;
    private float lastAttack;

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        if (Time.time >= lastAttack + cooldown)
        {
            
            lastAttack = Time.time;
        }
    }

    public override void Exit() { }
}
