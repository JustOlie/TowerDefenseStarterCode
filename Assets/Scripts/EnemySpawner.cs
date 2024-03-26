
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> Enemies;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnStartWaveButtonClicked()
    {
        StartEnemySpawning(); // Start met het spawnen van vijanden wanneer de knop wordt geklikt
    }

    private void StartEnemySpawning()
    {
        // Begin met het spawnen van vijanden
        InvokeRepeating("SpawnEnemy", 2f, 2f);
    }

    public void SpawnEnemy()
    {
        // Kies een willekeurig pad
        List<GameObject> selectedPath = (Random.Range(0, 2) == 0) ? Path1 : Path2;

        if (selectedPath.Count == 0)
        {
            Debug.LogError("Selected path is not assigned or empty.");
            return;
        }

        // Kies een willekeurig type vijand
        int enemyType = Random.Range(0, Enemies.Count);

        // Start bij het eerste punt van het pad
        GameObject newEnemy = Instantiate(Enemies[enemyType], selectedPath[0].transform.position, selectedPath[0].transform.rotation);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();

        // Stel het pad in voor de vijand
        enemyScript.SetPath((PathEnum.Path)(selectedPath == Path1 ? 0 : 1));

        // Start de vijand bij het eerste punt van het pad
        enemyScript.SetTarget(selectedPath[0]);
    }
}
