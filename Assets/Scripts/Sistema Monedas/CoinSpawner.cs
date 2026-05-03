using UnityEngine;
using UnityEngine.AI;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public CoinManager coinManager;
    public LayerMask playerLayer;

    public int numberOfCoins = 20; //Número de monedas a generar, por defecto 20 pero se puede cambiar desde el inspector

    public Vector3 center; //Centro del mapa (el área donde se generarán las monedas)
    public Vector3 size; //Tamaño del mapa

    public float navMeshSearchRadius = 1f; //Radio para buscar un punto válido para spawnear monedas en el NavMesh (es decir, dentro de un radio de 1 alrededor del punto aleatorio escogido y si hay NavMesh en ese radio sí se generará
    public float minDistanceBetweenCoins = 2f; //Distancia mínima entre monedas
    public float coinHeight = 1f; //Altura para que no queden enterradas en el suelo

    // NUEVO → evitar que aparezcan dentro de muebles/obstáculos
    public LayerMask obstacleLayer;
    public float obstacleCheckRadius = 0.8f;

    private void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        int coinsSpawned = 0; //Contador de monedas generadas correctamente
        int attempts = 0; //Número de intentos realizados
        int maxAttempts = 1000; //Límite para evitar un bucle infinito

        Vector3[] spawnedPositions = new Vector3[numberOfCoins];  //Array donde guardamos posiciones de generación de monedas ya usadas

        while (coinsSpawned < numberOfCoins && attempts < maxAttempts)
        {
            attempts++;

            Vector3 randomPoint = GetRandomPointInArea(); //Generamos un punto aleatorio dentro del área

            //Intentamos encontrar el punto más cercano dentro del NavMesh al aleatorio generado
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, navMeshSearchRadius, NavMesh.AllAreas)) //NavMesh.SamplePosition devuelve true si encuentra un punto válido dentro del radio especificado, y ese punto se guarda en 'hit'
            {
                Vector3 spawnPosition = hit.position;  //Si encuentra un punto válido, usamos esa posición

                //Comprobamos que no esté demasiado cerca de otras monedas
                // Y además que no esté dentro de un obstáculo
                if (IsFarEnough(spawnPosition, spawnedPositions, coinsSpawned) && !IsInsideObstacle(spawnPosition))
                {
                    //Creamos la moneda en esa posición
                    GameObject coin = Instantiate(
                        coinPrefab,
                        spawnPosition + Vector3.up * coinHeight, //La elevamos un poco
                        coinPrefab.transform.rotation //Usamos la rotación del prefab (en vez de Quaternion.identity)
                    );

                    Coin coinScript = coin.GetComponent<Coin>(); //Obtenemos el script Coin de la moneda

                    if (coinScript != null)
                    {
                        //Le pasamos referencias necesarias
                        coinScript.coinManager = coinManager;
                        coinScript.playerLayer = playerLayer;
                    }

                    spawnedPositions[coinsSpawned] = spawnPosition;  //Guardamos la posición usada para no generar monedas demasiado cerca en el futuro

                    coinsSpawned++;
                }
            }
        }
    }

    //Genera un punto aleatorio dentro del área definida (center + size)
    Vector3 GetRandomPointInArea()
    {
        float x = UnityEngine.Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float z = UnityEngine.Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);

        return new Vector3(x, center.y, z);
    }

    //Comprueba que la nueva moneda no esté demasiado cerca de otras
    bool IsFarEnough(Vector3 position, Vector3[] spawnedPositions, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (Vector3.Distance(position, spawnedPositions[i]) < minDistanceBetweenCoins)
            {
                return false;
            }
        }

        return true; //Posición válida
    }

    // NUEVO → comprueba si la moneda caería dentro de un mueble/obstáculo
    bool IsInsideObstacle(Vector3 position)
    {
        return Physics.CheckSphere(
            position,
            obstacleCheckRadius,
            obstacleLayer
        );
    }

    //Dibuja el área de generación en la escena (solo en editor) para poder ajustarla más fácilmente luego
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);

        // NUEVO → ver radio de comprobación de obstáculos
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, obstacleCheckRadius);
    }
}