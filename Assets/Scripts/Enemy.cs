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

    private GameManager gameManager;


    // Set het pad voor de vijand
    public void SetPath(PathEnum.Path newPath)
    {
        path = newPath;
        waypoints = (path == PathEnum.Path.Path1) ? EnemySpawner.Instance.Path1 : EnemySpawner.Instance.Path2;
    }



    public void Damage(int damage)
    {
        // Verlaag de gezondheidswaarde
        health -= damage;

        // Als de gezondheid kleiner of gelijk is aan nul
        if (health <= 0)
        {
            // Vernietig het game object
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetTarget(waypoints[currentWaypointIndex]);
        gameManager = GameManager.instance;
    }

    // Set het doelwit voor de vijand
    public void SetTarget(GameObject newTarget)
    {
        currentWaypointIndex = waypoints.IndexOf(newTarget);
    }
    private void OnDestroy()
    {
        // Get the remaining health of the enemy and convert it to an integer
        int remainingHealth = (int)GetComponent<Enemy>().health;

        // Check if this is the last enemy on the path
        if (isLastEnemyOnPath())
        {
            // Reduce the player's health
            GameManager.instance.ReduceHealth(remainingHealth);
        }
    }

    private bool isLastEnemyOnPath()
    {
        // Implementeer hier de logica om te controleren of dit de laatste vijand op het pad is
        // Dit hangt af van hoe je vijanden beheert en of je een manier hebt om te weten dat dit de laatste vijand is
        // Dit kan variëren afhankelijk van je implementatie
        // Een mogelijke aanpak is om te controleren of dit GameObject het laatste in de lijst is, als je een lijst van vijanden hebt
        // Of je kunt een teller bijhouden die wordt verlaagd wanneer een vijand wordt vernietigd, en controleer of deze 0 is
        // Voor demonstratiedoeleinden zal ik een vereenvoudigde aanpak gebruiken waarbij we ervan uitgaan dat dit altijd de laatste vijand is op het pad
        return true;
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
