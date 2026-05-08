using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public NavMeshAgent agent;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int currentPatrolIndex;

    [Header("Detection")]
    public Transform player;
    public float detectionRange = 10f;
    public float loseRange = 15f;

    private State currentState;

    //

    public int vida = 100;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new PatrolState(this));
    }

    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }







    [Header("Visión")]
    public float viewAngle = 100f;     // Ángulo total del cono de visión (ej: 100º)
    public float viewDistance = 10f;   // Distancia máxima a la que puede ver

    [Header("Capas")]
    public LayerMask playerLayer;     // Capa donde está el jugador
    public LayerMask obstacleLayer;   // Capa de obstáculos (paredes, etc.)





    //Aquí para dañar
    [HideInInspector]
    public Atacar attackController;



    public void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
        
    }

    public bool CheckVision()
    {
        if (player == null) return false;

        // ---------- DIRECCIÓN Y DISTANCIA ----------

        // Vector desde el enemigo hacia el jugador (dirección)
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        // Distancia real entre enemigo y jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Primer filtro: comprobar distancia (extra seguridad)
        if (distanceToPlayer > viewDistance)
            return false; // Si está demasiado lejos, no seguimos comprobando

        // ---------- FILTRO DE ÁNGULO ----------

        // Calcula el ángulo entre hacia dónde mira el enemigo y el jugador
        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);

        // Si está dentro del cono de visión (mitad a cada lado)
        if (angleToPlayer < viewAngle / 2f)
        {
            // ---------- FILTRO DE OBSTÁCULOS (RAYCAST) ----------

            // Lanza un rayo desde el enemigo hacia el jugador
            // Si NO choca con obstáculos antes de llegar, lo ve
            if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstacleLayer))
            {
                return true;
                //Debug.Log("Jugador detectado"); // VISIÓN EXITOSA
            }
            else
            {
                return false;
                //Debug.Log("Hay una pared en medio"); // Algo bloquea la visión
            }
        }
        return false;
    }

    public void AlertByTrap(Transform detectedPlayer)
    {
        player = detectedPlayer;
        ChangeState(new ChaseState(this));
    }

    private void OnDrawGizmos()
    {
        // Dibuja el rango de visión en la escena (esfera)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        // Calcula los límites del ángulo (izquierda y derecha)
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;

        // Dibuja las líneas del cono de visión
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftBoundary * viewDistance);
        Gizmos.DrawRay(transform.position, rightBoundary * viewDistance);
    }








}
