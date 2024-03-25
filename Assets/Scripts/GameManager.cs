using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int currentCredits = 0;
    private int currentHealth = 100;
    private int currentWave = 1;
    public List<GameObject> Archers;
    public List<GameObject> Swords;
    public List<GameObject> Wizards;

    private ConstructionSite selectedSite;
    public TopMenu topMenu; // Referentie naar het TopMenu-script

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
        topMenu.SetCreditsLabel("Credits: " + currentCredits);
        topMenu.SetHealthLabel("Health: " + currentHealth);
        topMenu.SetWaveLabel("Wave: " + currentWave);
    }

    public void AttackGate()
    {
        // Verminder de gezondheid met 1
        currentHealth--;

        // Update de label in TopMenu
        topMenu.SetHealthLabel("Health: " + currentHealth);
    }

    public void AddCredits(int amount)
    {
        // Update de credits
        currentCredits += amount;

        // Update de label in TopMenu
        topMenu.SetCreditsLabel("Credits: " + currentCredits);

        // Controleer de torenmenu. Dit doet voorlopig niets,
        // maar we zullen binnenkort code toevoegen om te controleren op credits
    }

    public void RemoveCredits(int amount)
    {
        // Vergelijkbaar met de vorige functie
        currentCredits -= amount;

        // Update de label in TopMenu
        topMenu.SetCreditsLabel("Credits: " + currentCredits);
    }

    public int GetCredits()
    {
        return currentCredits;
    }

    public int GetCost(PathEnum.Towers type, PathEnum.SiteLevel level, bool selling = false)
    {
        // Return de kosten voor elk type toren
        // De retourwaarde moet lager zijn als je verkoopt
    }
}
