
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public List<GameObject> Path1;
    public List<GameObject> Path2;
    public List<GameObject> Enemies;
    private int ufoCounter = 0;


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

    public void SpawnEnemy(int enemyType, List<GameObject> selectedPath)
    {
        if (selectedPath.Count == 0)
        {
            Debug.LogError("Selected path is not assigned or empty.");
            return;
        }

        // Start at the first point of the path
        GameObject newEnemy = Instantiate(Enemies[enemyType], selectedPath[0].transform.position, selectedPath[0].transform.rotation);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();

        // Set the path for the enemy
        enemyScript.SetPath((PathEnum.Path)(selectedPath == Path1 ? 0 : 1));

        // Start the enemy at the first point of the path
        enemyScript.SetTarget(selectedPath[0]);
    }

    public void StartWave(int number)
    {
        ufoCounter = 0; // Reset counter

        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
                // Add more cases for additional waves as needed
        }
    }

    public void StartWave1()
    {
        ufoCounter++;

        // Leave some gaps
        if (ufoCounter % 6 <= 1) return;

        if (ufoCounter < 30)
        {
            EnemySpawner.Instance.SpawnEnemy(0, EnemySpawner.Instance.Path1);
        }
        else
        {
            // The last Enemy will be level 2
            EnemySpawner.Instance.SpawnEnemy(1, EnemySpawner.Instance.Path2);
        }

        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1"); // Stop the wave spawning
            GameManager.instance.EndWave(); // Notify GameManager that the wave has ended
        }
    }
}
