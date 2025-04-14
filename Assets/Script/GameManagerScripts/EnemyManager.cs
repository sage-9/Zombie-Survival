using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManager", menuName = "Scriptable Objects/EnemyManager")]
public class EnemyManager : ScriptableObject
{
    public Transform PlayerLocation;
    public GameObject EnemyPrefab;
    public GameObject EnemyParent;
    public int ActiveEnemyLimit;
    public Vector3[] spawnPositions;
    public  List<GameObject> ActiveEnemies;
   
    public void InstantiateEnemy()
    {
        Instantiate(EnemyPrefab,spawnPositions[RandomNumberGenerator.GetInt32(0,spawnPositions.Length)],EnemyPrefab.transform.rotation);
    }
    
    void HandleWaveLogic()
    {
        if (ActiveEnemies.Count<ActiveEnemyLimit)
        {
            InstantiateEnemy();
        }
    }
    

}

