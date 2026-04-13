using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorirState : State
{

    public MorirState(NPCController npc) : base(npc) { }


    //tiempo para desaparexcer 
    private float deathTimer = 2f;

    // Update is called once per frame
    public override void Update()
    {
        deathTimer -= Time.time;

        if (deathTimer <= 0)
        {
            GameObject.Destroy(npc.gameObject);
        }
        
    }


    public override void Enter()
    {
        //deja de patrullar y empieza a perseguir
        npc.agent.isStopped = false;

        //
        npc.agent.enabled = false;
        npc.GetComponent<Collider>().enabled = false;
    }


    public override void Exit() { }
}
