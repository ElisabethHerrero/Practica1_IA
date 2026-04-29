using UnityEngine; 

public class TrapDetection : MonoBehaviour
{
    public NPCController[] npcsToAlert; //Lista de NPCs que serán avisados cuando se active la trampa
    public LayerMask playerLayer; //Layer del jugador para detectar solo al player

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

            foreach (NPCController npc in npcsToAlert) //Avisamos a todos los NPCs de la lista
            {
                if (npc != null)
                {
                    npc.AlertByTrap(other.transform); //Persigue al jugador
                }
            }
          
            trapCollider.enabled = false;   //Desactivamos el collider para que no se vuelva a activar inmediatamente

            isOnCooldown = true; //Activamos el cooldown
            timer = cooldownTime;
        }
    }

    void ResetTrap() //Método que reactiva la trampa después del cooldown
    {
        activated = false;
        isOnCooldown = false;
        trapCollider.enabled = true;
    }
}