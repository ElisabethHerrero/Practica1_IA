using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFactory : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject guardPrefab;
    public GameObject zombiePrefab;

    [Header("Data")]
    public NPCData guardData;
    public NPCData zombieData;

    public NPCController CreateNPC(
        NPCType type,
        Vector3 position,
        Transform[] patrolPoints)
    {
        GameObject prefab = null;
        NPCData data = null;

        switch (type)
        {
            case NPCType.Guard:
                prefab = guardPrefab;
                data = guardData;
                break;

            case NPCType.Zombie:
                prefab = zombiePrefab;
                data = zombieData;
                break;
        }

        GameObject npcObj = Instantiate(prefab, position, Quaternion.identity);

        NPCController npc = npcObj.GetComponent<NPCController>();

        npc.Initialize(data, patrolPoints);

        return npc;
    }
}
