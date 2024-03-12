using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public int points = 1;
    public PathEnum.Path path { get; private set; }
    public List<GameObject> waypoints;
    private int currentWaypointIndex = 0;

    // Set het pad voor de vijand
    public void SetPath(PathEnum.Path newPath)
    {
        path = newPath;
        waypoints = (path == PathEnum.Path.Path1) ? EnemySpawner.Instance.Path1 : EnemySpawner.Instance.Path2;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTarget(waypoints[currentWaypointIndex]);
    }

    // Set het doelwit voor de vijand
    public void SetTarget(GameObject newTarget)
    {
        currentWaypointIndex = waypoints.IndexOf(newTarget);
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints == null || waypoints.Count == 0)
            return;

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, step);

        // Controleer of de vijand het doelwit heeft bereikt
        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < 0.1f)
        {
            // Ga naar het volgende waypoint als deze beschikbaar is
            if (currentWaypointIndex < waypoints.Count - 1)
            {
                currentWaypointIndex++;
                SetTarget(waypoints[currentWaypointIndex]);
            }
            else
            {
                // Als alle waypoints zijn bereikt, vernietig de vijand
                Destroy(gameObject);
            }
        }
    }
}
