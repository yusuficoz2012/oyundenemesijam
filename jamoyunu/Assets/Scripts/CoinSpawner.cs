using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Coin prefab referans�
    public int coinCount = 20;    // Ka� tane coin spawnlans�n
    public Vector3 areaSize = new Vector3(20f, 1f, 20f); // Spawn alan� (X-Z)

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                transform.position.y,
                Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
            );

            Vector3 spawnPos = transform.position + randomPosition;

            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
