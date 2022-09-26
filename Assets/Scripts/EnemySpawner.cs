using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWaveConfig;

    bool isLooping = true;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator SpawnEnemyWaves()
    {

        do
        {
            foreach(WaveConfigSO waveConfig in waveConfigs)
            {
                currentWaveConfig = waveConfig;

                for(int i =0; i < currentWaveConfig.GetEnemyCount(); i++)
                {
                    Instantiate(currentWaveConfig.GetEnemyPrefab(i), currentWaveConfig.GetStartingWaypoint().position, Quaternion.identity, transform);

                    yield return new WaitForSecondsRealtime(currentWaveConfig.GetRandomSpawnTime());
                }

                yield return new WaitForSecondsRealtime(timeBetweenWaves);
            }
        }
        while(isLooping);
        

        
    }

    public WaveConfigSO GetCurrentWaveConfig()
    {
        return currentWaveConfig;
    }
}
