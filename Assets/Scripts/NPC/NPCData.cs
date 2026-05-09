using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/NPC Data")]
public class NPCData : ScriptableObject
{
    public int vida;
    public float speed;
    public float detectionRange;
    public float loseRange;
    public float viewAngle;
    public float viewDistance;
}