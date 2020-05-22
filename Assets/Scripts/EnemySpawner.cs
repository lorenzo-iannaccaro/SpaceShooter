using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float timeBetweenWavesSpawn = 2f;
    int startWaveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //var currentWave = waveConfigs[startWaveIndex];
        //StartCoroutine(SpawnEnemiesInWave(currentWave));
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnWaves()
    {
        for(int i = startWaveIndex; i < waveConfigs.Count; i++)
        {
            StartCoroutine(SpawnEnemiesInWave(waveConfigs[i]));
            yield return new WaitForSeconds(timeBetweenWavesSpawn);
        }
    }
    private IEnumerator SpawnEnemiesInWave(WaveConfig waveConfig)
    {
        for(int i = 0; i < waveConfig.GetEnemiesPerWave(); i++)
        {
            GameObject enemy = Instantiate(waveConfig.GetEnemyPrefab(),
                    waveConfig.GetWaypoints()[0].transform.position,
                    Quaternion.identity);
            enemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenEnemySpawn());
        }
    }
}
