﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy wave configuration")] 

public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] int enemiesPerWave = 5;
    [SerializeField] float enemyMoveSpeed = 5f;
    [SerializeField] float timeBetweenSpawn = 0.4f;
    [SerializeField] float spawnTimeRandomicity = 0.2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    public List<Transform> GetWaypoints()
    {
        var waypoints = new List<Transform>();
        foreach(Transform waypoint in pathPrefab.transform){
            waypoints.Add(waypoint);
        }

        return waypoints;
    }
    public int GetEnemiesPerWave()
    {
        return enemiesPerWave;
    }
    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }
    public float GetTimeBetweenSpawn()
    {
        return timeBetweenSpawn;
    }
    public float GetSpawnTimeRandomicity()
    {
        return spawnTimeRandomicity;
    }


}