using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected NPCController npc;

    public State(NPCController npc)
    {
        this.npc = npc;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();

















}
