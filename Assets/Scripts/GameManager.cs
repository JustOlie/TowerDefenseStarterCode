using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int currentCredits = 0;
    private int currentHealth = 100;
    private int currentWave = 0;
    public int numberOfWaves = 5; 
    public List<GameObject> Archers;
    public List<GameObject> Swords;
    public List<GameObject> Wizards;

    private ConstructionSite selectedSite;
    public GameObject topMenu; // Referentie naar het TopMenu-object
    public GameObject menu; // Referentie naar het menu-object


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Vind en wijs het TopMenu-object toe
        if (topMenu == null)
        {
            Debug.LogError("TopMenu niet toegewezen in de Inspector!");
        }
    }

    public void StartWave(int waveIndex)
    {
        // Incrementeer de huidige wave
        currentWave++;

        // Update de labels in TopMenu met de huidige wave
        if (topMenu != null)
        {
            topMenu.GetComponent<TopMenu>().SetWaveLabel("Wave: " + currentWave);
        }
        else
        {
            Debug.LogError("TopMenu is niet toegewezen in de Inspector!");
        }

        // Implementeer de logica om vijanden te laten spawnen
        SpawnEnemiesForWave(currentWave);

        // Als het het laatste wave is, stop dan de wave-sequentie
        if (currentWave >= numberOfWaves)
        {
            Debug.Log("Laatste wave bereikt! Spel is afgelopen.");
            // Voeg hier eventuele logica toe voor het einde van het spel
        }
    }

    public void StartFirstWave()
    {
        StartWave(1);
    }
    private void SpawnEnemiesForWave(int waveIndex)
    {
        // Implementeer hier logica om vijanden te spawnen voor de gegeven wave
        // Dit kan variëren afhankelijk van je spellogica en design
        // Je kunt bijvoorbeeld een bepaald aantal vijanden spawnen en de typen vijanden variëren naarmate de waves vorderen
        // Hier is een eenvoudige implementatie voor demonstratiedoeleinden:

        int numberOfEnemies = waveIndex * 5; // Bijvoorbeeld, elke wave heeft 5 meer vijanden dan de vorige wave

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Roep SpawnEnemy() aan uit EnemySpawner of implementeer de logica hier indien nodig
            EnemySpawner.Instance.SpawnEnemy();
        }
    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        // Stel de beginwaarden in
        currentCredits = 200;
        currentHealth = 10;
        currentWave = 0;

        // Update de labels in TopMenu
        if (topMenu != null)
        {
            topMenu.GetComponent<TopMenu>().SetCreditsLabel("Credits: " + currentCredits);
            topMenu.GetComponent<TopMenu>().SetHealthLabel("Health: " + currentHealth);
            topMenu.GetComponent<TopMenu>().SetWaveLabel("Wave: " + currentWave);
        }
        else
        {
            Debug.LogError("TopMenu is niet toegewezen in de Inspector!");
        }
    }

    public void SelectSite(ConstructionSite site)
    {
        selectedSite = site;
        // Open het menu wanneer een site is geselecteerd
        //OpenMenu();

        // Hier verkrijg je een referentie naar TowerMenu via GetComponent
        TowerMenu towerMenu = menu.GetComponent<TowerMenu>();
        if (towerMenu != null)
        {
            towerMenu.SetSite(selectedSite);
        }
    }

    public void Build(PathEnum.Towers type, PathEnum.SiteLevel level)
    {
        // Controleer of er een site is geselecteerd
        // Controleer of er een site is geselecteerd
        if (selectedSite == null)
        {
            Debug.LogWarning("Er is geen site geselecteerd om te bouwen.");
            return;
        }

        // Bepaal de kosten van de toren
        int cost = GetCost(type, level);

        // Controleer of er genoeg credits zijn om de toren te bouwen
        if (currentCredits < cost)
        {
            Debug.LogWarning("Onvoldoende credits om de toren te bouwen.");
            return;
        }

        // Verlaag de credits met de kosten van de toren
        RemoveCredits(cost);

        // Selecteer de juiste lijst met prefab-torens op basis van het type toren
        List<GameObject> towerList = null;
        switch (type)
        {
            case PathEnum.Towers.Archer:
                towerList = Archers;
                break;
            case PathEnum.Towers.Sword:
                towerList = Swords;
                break;
            case PathEnum.Towers.Wizard:
                towerList = Wizards;
                break;
            default:
                Debug.LogError("Ongeldig torentype: " + type);
                return;
        }

        // Controleer of de lijst met prefab-torens is toegewezen
        if (towerList == null || towerList.Count == 0)
        {
            Debug.LogError("Er zijn geen prefab-torens beschikbaar voor het opgegeven type: " + type);
            return;
        }

        // Controleer of het opgegeven niveau binnen het bereik van de prefab-torenlijst ligt
        if ((int)level < 0 || (int)level >= towerList.Count)
        {
            Debug.LogError("Ongeldig niveau voor het opgegeven torentype: " + level);
            return;
        }

        // Maak een toren uit de lijst van prefab-torens op basis van het opgegeven niveau
        GameObject towerPrefab = towerList[(int)level];
        if (towerPrefab == null)
        {
            Debug.LogError("Prefab-toren op niveau " + level + " is niet toegewezen.");
            return;
        }

        // Plaats de toren op de geselecteerde site
        GameObject newTower = Instantiate(towerPrefab, selectedSite.WorldPosition, Quaternion.identity);
        selectedSite.SetTower(newTower, level, type);

        // Verberg het menu door null door te geven aan de SetSite-functie in TowerMenu
        TowerMenu.instance.SetSite(null);
    }

    public void AttackGate()
    {
        // Verminder de gezondheid met 1
        currentHealth--;

        // Update de label in TopMenu
        if (topMenu != null)
        {
            topMenu.GetComponent<TopMenu>().SetHealthLabel("Health: " + currentHealth);
        }
        else
        {
            Debug.LogError("TopMenu is niet toegewezen in de Inspector!");
        }
    }

    public void AddCredits(int amount)
    {
        // Update de credits
        currentCredits += amount;

        // Update de label in TopMenu
        if (topMenu != null)
        {
            topMenu.GetComponent<TopMenu>().SetCreditsLabel("Credits: " + currentCredits);
        }
        else
        {
            Debug.LogError("TopMenu is niet toegewezen in de Inspector!");
        }
    }
    public void RemoveCredits(int amount)
    {
        // Vergelijkbaar met de vorige functie
        currentCredits -= amount;

        // Update de label in TopMenu
        if (topMenu != null)
        {
            topMenu.GetComponent<TopMenu>().SetCreditsLabel("Credits: " + currentCredits);
        }
        else
        {
            Debug.LogError("TopMenu is niet toegewezen in de Inspector!");
        }
    }

    public int GetCredits()
    {
        return currentCredits;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetCurrentWaveIndex()
    {
        return currentWave;
    }
    public int GetCost(PathEnum.Towers type, PathEnum.SiteLevel level, bool selling = false)
    {
        int cost = 0;

        // Bepaal de kosten op basis van het type toren en het niveau van de site
        switch (type)
        {
            case PathEnum.Towers.Archer:
                cost = (selling ? 50 : 100); // Bij verkoop 50 credits minder dan bij aankoop
                break;
            case PathEnum.Towers.Sword:
                cost = (selling ? 75 : 150);
                break;
            case PathEnum.Towers.Wizard:
                cost = (selling ? 100 : 200);
                break;
            // Voeg andere torens toe zoals nodig
            default:
                Debug.LogError("Ongeldig torentype: " + type);
                break;
        }

        // Voeg extra kosten toe op basis van het niveau van de site, als nodig
        switch (level)
        {
            case PathEnum.SiteLevel.level1:
                cost += 0; // Geen extra kosten voor niveau 1
                break;
            case PathEnum.SiteLevel.level2:
                cost += 50; // 50 extra kosten voor niveau 2
                break;
            case PathEnum.SiteLevel.level3:
                cost += 100; // 100 extra kosten voor niveau 3
                break;
            // Voeg andere niveaus toe zoals nodig
            default:
                Debug.LogError("Ongeldig sitelevel: " + level);
                break;
        }

        return cost;
    }
    public void ReduceHealth(int amount)
    {
        // Verminder de gezondheid van de speler met het opgegeven bedrag
        currentHealth -= amount;

        // Update de label in TopMenu
        if (topMenu != null)
        {
            topMenu.GetComponent<TopMenu>().SetHealthLabel("Health: " + currentHealth);
        }
        else
        {
            Debug.LogError("TopMenu is niet toegewezen in de Inspector!");
        }

        // Controleer of de speler geen gezondheid meer heeft
        if (currentHealth <= 0)
        {
            // Implementeer logica voor game over
            Debug.Log("Game over! De poort is vernietigd.");
        }
    }
}

