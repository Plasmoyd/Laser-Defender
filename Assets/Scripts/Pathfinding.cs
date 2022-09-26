using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    List<Transform> waypoints;
    int currentWaypointIndex = 0;

    WaveConfigSO waveConfig;
    EnemySpawner enemySpawner;

    void Awake() 
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWaveConfig();
        waypoints = waveConfig.GetWaypoints();
        SetStartingPosition();
    }

    void Update()
    {
        FollowPath();
    }

    private void SetStartingPosition()
    {
        transform.position = waypoints[currentWaypointIndex].position;
    }

    private void FollowPath()
    {
        if(currentWaypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[currentWaypointIndex].position;
            Vector2 currentPosition = transform.position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;

            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, delta);
            if(transform.position == targetPosition)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
