using System;
using UnityEngine;


public class WaveManager : MonoBehaviour 
{
    public Wave wave;
    public GameObject zombie;

    public int enemies;
    public int ActiveEnemies;
    public Transform spawnPositions;

    void OnEnable()
    {
        CreateWave();
        EnemyHealth.OnDie+=wave.handleDeadEnemies;        
    }
    void OnDisable()
    {
        EnemyHealth.OnDie-=wave.handleDeadEnemies; 
    }
    
    void Start()
    {
        Wave.waveCounter=0;
      
        
    }

    void CreateWave()
    {
        wave=new Wave(enemies,ActiveEnemies,zombie);   
             
    }
    void Update()
    {
        if (wave==null)CreateWave();
        if(wave.ActiveEnemiesCount<ActiveEnemies)
        {
            wave.spawnEnemies(spawnPositions);
        }
        if(wave.hasCompleted)HandleWaveCompletion();

    }
    void HandleWaveCompletion()
    {
        wave=null;
        ActiveEnemies+=2;
        enemies+=5;
    }    
}



