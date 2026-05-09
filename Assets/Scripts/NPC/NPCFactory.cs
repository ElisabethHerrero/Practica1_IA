using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFactory : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject SlimePrefab;
    public GameObject GoshtPrefab;

    [Header("Data")]
    public NPCData SlimeData;
    public NPCData GhostData;

    public NPCController CreateNPC(
        NPCType type,
        Vector3 position,
        Transform[] patrolPoints)
    {
        GameObject prefab = null;
        NPCData data = null;

        switch (type)
        {
            case NPCType.Slime:
                prefab = SlimePrefab;
                data = SlimeData;
                break;

            case NPCType.Fantasma:
                prefab = GoshtPrefab;
                data = GhostData;
                break;
        }

        GameObject npcObj = Instantiate(prefab, position, Quaternion.identity);

        NPCController npc = npcObj.GetComponent<NPCController>();

        npc.Initialize(data, patrolPoints);

        return npc;
    }
}
