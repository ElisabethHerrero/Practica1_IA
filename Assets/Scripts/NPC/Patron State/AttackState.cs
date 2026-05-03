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
        Debug.Log("Voy a atacar");
        Attack = npc.Attack;
    }

    public override void Update()
    {
        if (Time.time >= lastAttack + cooldown)
        {
            
            lastAttack = Time.time;
        }

        if (npc.DistanceToPlayer() > 10f)
        {
            npc.ChangeState(new ChaseState(npc));
        }

       
    }

    public Collider Attack;
    

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Está atacando");

        if (other == Attack)
        {
            Debug.Log("Está atacando2");
            

        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Está atacando3");
            other.GetComponent<VidaPlayer>()?.CogerDaño(20);
        }
    }

    public override void Exit() { }
}
