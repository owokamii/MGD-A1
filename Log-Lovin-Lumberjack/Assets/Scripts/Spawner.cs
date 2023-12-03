using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] logPrefabs;
    public GameObject dynamitePrefab;

    [Range(0f, 1f)]
    public float dynamiteChance = 0.05f;

    public float gameStartDelay = 2f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    public float minAngle = -15f;
    public float maxAngle = 15f;

    public float minForce = 18f;
    public float maxForce = 22f;

    public float maxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(gameStartDelay);

        while(enabled)
        {
            GameObject prefab = logPrefabs[Random.Range(0, logPrefabs.Length)];

            if(Random.value < dynamiteChance)
            {
                prefab = dynamitePrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject log = Instantiate(prefab, position, rotation);
            Destroy(log, maxLifetime);

            float force = Random.Range(minForce, maxForce);
            log.GetComponent<Rigidbody>().AddForce(log.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay)); 
        }    
    }
}
