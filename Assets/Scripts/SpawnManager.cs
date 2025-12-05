using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public float spawnRange;
    private int waveSize = 1;

    void Start()
    {
      
    }
   
    void SpawnEnemyWave(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {  // Spawn enemy
            Instantiate(enemyPrefab, GetSpawnPoint(), enemyPrefab.transform.rotation);
        }

        // Spawn powerup
        Instantiate(powerupPrefab, GetSpawnPoint(), powerupPrefab.transform.rotation);

    }

    Vector3 GetSpawnPoint()
    {
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);
        return new Vector3(x, 0, z);

     }


    // Update is called once per frame
    void Update()
    {
        int survivingEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if (survivingEnemies == 0)
        {
            SpawnEnemyWave(waveSize);
            waveSize++;
        }
    }

}