using UnityEngine;

public class MosquitoSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject mosquitoPrefab;   // Mosquito prefab
    public float spawnInterval = 2f;    // Time between spawns
    public int maxMosquitoes = 10;      // Limit active mosquitoes
    public float spawnDistance = 8f;    // Distance in front of player
    public float spawnWidth = 3f;       // Horizontal spread
    public float spawnHeight = 2f;      // Vertical spread

    private float spawnTimer;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        HandleSpawning();
    }

    void HandleSpawning()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && CountActiveMosquitoes() < maxMosquitoes)
        {
            SpawnMosquito();
            spawnTimer = 0f;
        }
    }

    void SpawnMosquito()
    {
        // Spawn in a zone IN FRONT of player
        Vector3 forward = player.forward;

        Vector3 spawnPos = player.position +
                           forward * spawnDistance +
                           new Vector3(Random.Range(-spawnWidth, spawnWidth),
                               Random.Range(1f, spawnHeight),
                               0f);

        Instantiate(mosquitoPrefab, spawnPos, Quaternion.identity);
    }

    int CountActiveMosquitoes()
    {
        return GameObject.FindGameObjectsWithTag("Mosquito").Length;
    }
}