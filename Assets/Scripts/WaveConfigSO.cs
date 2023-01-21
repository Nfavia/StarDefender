using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;

    [Header("Movement Settings")] 
    [SerializeField] float baseMoveSpeed = 5f;
    [SerializeField] bool speedChangesDuringPath = false;
    [SerializeField] int speedChangeStartPos;
    [SerializeField] int speedChangeEndPos;
    [SerializeField] float speedChangeValue = 2f;
    int currentWaypointIndex = -1;

    [Header("Spawn Settings")]
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minSpawnTime = .2f;

    private void Awake()
    {
        //speedChangeEndPos += 1;
        //speedChangeStartPos += 2;
    }

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        if(!speedChangesDuringPath)
            return baseMoveSpeed;
        else
        {
            float speed = baseMoveSpeed;

            if (currentWaypointIndex > speedChangeStartPos &&
                currentWaypointIndex < speedChangeEndPos)
            {
                speed = speedChangeValue;
            }
                

            return speed;
        }
    }

    public void SetCurentWaypointIndex(int index)
    {
        currentWaypointIndex = index;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        float spawntime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, 
                                        timeBetweenEnemySpawns + spawnTimeVariance);

        return Mathf.Clamp(spawntime, minSpawnTime, float.MaxValue);
    }

    //public bool DoesSpeedChange()
    //{
    //    return speedChangesDuringPath;
    //}
}
