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

        [Header("Visi�n")]
    public float viewAngle = 100f; //angulo total del cono de visi�n (ej: 100�)
    public float viewDistance = 10f; //Distancia m�xima a la que puede ver

    [Header("Capas")]
    public LayerMask playerLayer; //Capa donde est� el jugador
    public LayerMask obstacleLayer; //Capa de obst�culos (paredes, etc.)

    private State currentState;

    //

    public int vida;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new PatrolState(this));
        attackController = GetComponent<Atacar>();
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

    //para la fabrica de NPCs

    public void Initialize(NPCData data, Transform[] patrols)
    {
        vida = data.vida;

        

        patrolPoints = patrols;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;

        // VIDA
        Vida vidaComponent = GetComponent<Vida>();

        if (vidaComponent != null)
        {
            vidaComponent.Initialize(data.vida);
        }

        // DA�O
        Armas weapon = GetComponentInChildren<Armas>();

        if (weapon != null)
        {
            weapon.Initialize(data.danio);
        }


    }


    //Aqu� para da�ar
    [HideInInspector]
    public Atacar attackController;


    public bool CheckVision()
    {
        if (player == null) return false;

        // DIRECCI�N Y DISTANCIA 

        // Vector desde el enemigo hacia el jugador (direcci�n)
        Vector3 dirToPlayer = (player.position - transform.position).normalized;

        // Distancia real entre enemigo y jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Primer filtro: comprobar distancia (extra seguridad)
        if (distanceToPlayer > viewDistance)
            return false; // Si est� demasiado lejos, no seguimos comprobando

        //  FILTRO DE �NGULO 

        // Calcula el �ngulo entre hacia d�nde mira el enemigo y el jugador
        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);

        // Si est� dentro del cono de visi�n (mitad a cada lado)
        if (angleToPlayer < viewAngle / 2f)
        {
            //  FILTRO DE OBST�CULOS (RAYCAST) 

            // Lanza un rayo desde el enemigo hacia el jugador
            // Si NO choca con obst�culos antes de llegar, lo ve
            if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstacleLayer))
            {
                return true;
                //Debug.Log("Jugador detectado"); // VISI�N EXITOSA
            }
            else
            {
                return false;
                //Debug.Log("Hay una pared en medio"); // Algo bloquea la visi�n
            }
        }
        return false;
    }

    public void AlertByTrap(Transform detectedPlayer)
    {
        player = detectedPlayer;
        ChangeState(new ChaseState(this));
    }

// para poder visualizarlo en la escena mejor
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, leftBoundary * viewDistance);
        Gizmos.DrawRay(transform.position, rightBoundary * viewDistance);
    }


    public void SetPlayer(Transform target)
    {
        player = target;
    }




}
