using UnityEngine;

public class TrapDetection : MonoBehaviour
{
    public LayerMask playerLayer; //Layer del jugador para detectar solo al player

    public float alertRadius = 10f; //Distancia máxima a la que la trampa avisa a los NPCs cercanos

    private bool activated = false; //Indica si la trampa ya ha sido activada
    public float cooldownTime = 10f; //Tiempo que tarda la trampa en reactivarse

    private float timer = 0f; //Contador para el cooldown
    private bool isOnCooldown = false; //Indica si la trampa está en recarga

    private Collider trapCollider; //Referencia al collider

    void Start()
    {
        trapCollider = GetComponent<Collider>(); //Guardamos el collider del objeto al iniciar
    }

    void Update()
    {
        if (isOnCooldown) //Si la trampa está en cooldown
        {
            timer -= Time.deltaTime; //Restamos tiempo cada frame (cuenta atrás)

            if (timer <= 0f)
            {
                ResetTrap();
            }
        }
    }

    private void OnTriggerEnter(Collider other) //Se ejecuta cuando algo entra en el trigger de la trampa
    {
        if (activated) return; //Si ya está activada no se hace nada

        if (((1 << other.gameObject.layer) & playerLayer) != 0) //Comprobación mediante layer de si es el jugador el que ha entrado en la trampa
        {
            activated = true;

            AlertNearbyNPCs(other.transform); //Avisamos automáticamente a los NPCs cercanos a la trampa

            trapCollider.enabled = false;   //Desactivamos el collider para que no se vuelva a activar inmediatamente

            isOnCooldown = true; //Activamos el cooldown
            timer = cooldownTime;
        }
    }

    void AlertNearbyNPCs(Transform playerTransform) //Busca todos los NPCs de la escena y avisa solo a los que están cerca
    {
        NPCController[] allNPCs = FindObjectsOfType<NPCController>(); //Encuentra todos los objetos de la escena que tengan NPCController

        foreach (NPCController npc in allNPCs) //Recorremos todos los NPCs encontrados
        {
            if (npc != null)
            {
                float distanceToTrap = Vector3.Distance(transform.position, npc.transform.position); //Calculamos la distancia entre la trampa y el NPC

                if (distanceToTrap <= alertRadius) //Si el NPC está dentro del radio de alerta
                {
                    npc.AlertByTrap(playerTransform); //Persigue al jugador
                }
            }
        }
    }

    void ResetTrap() //Método que reactiva la trampa después del cooldown
    {
        activated = false;
        isOnCooldown = false;
        trapCollider.enabled = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, alertRadius); //Dibuja el radio de alerta de la trampa en la escena
    }
}