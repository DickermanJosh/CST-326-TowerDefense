using System;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Vector3 destination = new Vector3(70f,2.5f,-70f);
    private void Start()
    {
        agent.SetDestination(destination);
    }

    private void Update()
    {
        if (transform.position != destination) return;
        PlayerStats.Lives--;
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;

    }

    /*private Transform target;
    private int wavepointIndex = 0;
    private Enemy enemy;
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.waypoints[0];
    }
    
    
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = Waypoints.waypoints[wavepointIndex];
    }
    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;
    }*/
}
