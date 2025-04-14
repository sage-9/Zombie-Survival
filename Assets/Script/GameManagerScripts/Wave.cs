using UnityEngine;
using System.Collections.Generic;

public class Wave
{
    public static int waveCounter;
    public int Wavenumber;
    int EnemiesLeftInWave;
    int NumberOfActiveEnemiesAtATime;
    public bool hasCompleted;
    public GameObject EnemyPrefab;

    public static List<GameObject> ActiveEnemies=new List<GameObject>();
    public int ActiveEnemiesCount;

    public Wave(int EnemiesInWave, int NumberOfActiveEnemiesAtATime,GameObject enemyPrefab)
    {
        waveCounter++;
        Wavenumber=waveCounter;
        this.EnemiesLeftInWave = EnemiesInWave;
        this.NumberOfActiveEnemiesAtATime = NumberOfActiveEnemiesAtATime;
        this.EnemyPrefab=enemyPrefab;
        ActiveEnemiesCount=ActiveEnemies.Count;
    }

    public void spawnEnemy(Transform spawnPosition)
    {
        GameObject zombie = GameObject.Instantiate(EnemyPrefab, spawnPosition.position,spawnPosition.rotation);
        ActiveEnemies.Add(zombie);

    }

    public void spawnEnemies(Transform spawnPosition)
    {
        if(EnemiesLeftInWave<=0 || ActiveEnemies.Count>=NumberOfActiveEnemiesAtATime) return;
        else
        {
            for(int i=0;i<=NumberOfActiveEnemiesAtATime;i++)
            {
                spawnEnemy(spawnPosition);
            }
        }
    }
    public void handleDeadEnemies(GameObject enemy)
    {
        EnemiesLeftInWave--;
        ActiveEnemies.Remove(enemy);
    }

    public void successCondition()
    {
        if(EnemiesLeftInWave<=0) hasCompleted = true;
    }
}
