using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    [Header("Factory")]
    public NPCFactory factory;

    [Header("NPC Settings")]
    public NPCType[] npcTypes;

    [Header("Spawn Control")]
    public int maxEnemies = 8;
    public float checkInterval = 2f;

    [Header("Spawn Distance")]
    public float minDistanceFromPlayer = 20f;
    public float maxDistanceFromPlayer = 40f;

    [Header("References")]
    public Transform player;

    [Header("Patrol Points")]
    public Transform[] allPatrolPoints;
    public int patrolPointsPerNPC = 3;

    [Header("NavMesh")]
    public float navMeshSearchRadius = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= checkInterval)
        {
            timer = 0f;

            CheckAndSpawn();
        }
    }

    void CheckAndSpawn()
    {
        // Cuenta enemigos actuales
        NPCController[] enemies = FindObjectsByType<NPCController>(FindObjectsSortMode.None);

        // Si ya hay suficientes, no spawnea
        if (enemies.Length >= maxEnemies)
            return;

        // Busca posición válida en el NavMesh
        Vector3 spawnPosition;

        if (GetRandomNavMeshPosition(out spawnPosition))
        {
            // Patrullas aleatorias
            Transform[] patrols =
                GetRandomPatrols(allPatrolPoints, patrolPointsPerNPC);

            NPCType randomType = npcTypes[Random.Range(0, npcTypes.Length)]; //para que sea aleatorio

            // Crear NPC
            factory.CreateNPC(
                randomType,
                spawnPosition,
                patrols);
        }
    }

    bool GetRandomNavMeshPosition(out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            // Dirección aleatoria alrededor del jugador
            Vector2 randomCircle =
                Random.insideUnitCircle.normalized *
                Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);

            Vector3 randomPosition = new Vector3(
                player.position.x + randomCircle.x,
                player.position.y,
                player.position.z + randomCircle.y);

            // Busca punto válido en NavMesh
            NavMeshHit hit;

            if (NavMesh.SamplePosition(
                randomPosition,
                out hit,
                navMeshSearchRadius,
                NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    Transform[] GetRandomPatrols(Transform[] allPoints, int amount)
    {
        List<Transform> selected = new List<Transform>();

        amount = Mathf.Min(amount, allPoints.Length);

        while (selected.Count < amount)
        {
            Transform randomPoint =
                allPoints[Random.Range(0, allPoints.Length)];

            if (!selected.Contains(randomPoint))
                selected.Add(randomPoint);
        }

        return selected.ToArray();
    }

    // Visualización en escena
    private void OnDrawGizmosSelected()
    {
        if (player == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, minDistanceFromPlayer);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.position, maxDistanceFromPlayer);
    }
}
