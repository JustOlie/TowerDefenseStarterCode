using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    public Label waveLabel;
    public Label creditsLabel;
    public Label healthLabel;
    public Button startWaveButton;

    private void Start()
    {
        // Zoek de root van het UI-document
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Vind de labels en de knop binnen de root
        waveLabel = root.Q<Label>("waveLabel");
        creditsLabel = root.Q<Label>("creditsLabel");
        healthLabel = root.Q<Label>("healthLabel");
        startWaveButton = root.Q<Button>("startWaveButton");

        // Voeg een luisteraar toe aan de StartWaveButton
        startWaveButton.clicked += OnStartWaveButtonClicked;

        // Update de topmenu-labels
        UpdateTopMenuLabels(GameManager.instance.GetCredits(), GameManager.instance.GetHealth(), GameManager.instance.GetCurrentWaveIndex());
    }

    // Functie om de labels bij te werken
    public void UpdateTopMenuLabels(int credits, int health, int wave)
    {
        Debug.Log("Updating top menu labels: Credits: " + credits + ", Health: " + health + ", Wave: " + wave);
        creditsLabel.text = "Credits: " + credits; // Update de creditslabel met de juiste waarde
        healthLabel.text = "Health: " + health;
        waveLabel.text = "Wave: " + wave;
    }

    // Functie om de credits-label bij te werken
    public void SetCreditsLabel(string text)
    {
        creditsLabel.text = text;
    }

    // Functie om de health-label bij te werken
    public void SetHealthLabel(string text)
    {
        healthLabel.text = text;
    }

    // Functie om de wave-label bij te werken
    public void SetWaveLabel(string text)
    {
        waveLabel.text = text;
    }

    // Functie die wordt aangeroepen wanneer de StartWaveButton wordt geklikt
    private void OnStartWaveButtonClicked()
    {
        // Incrementeer de huidige golfindex voordat de volgende golf wordt gestart
        int currentWaveIndex = GameManager.instance.GetCurrentWaveIndex();
        int nextWaveIndex = currentWaveIndex + 1;
        // Start de volgende golf
        GameManager.instance.StartWave(nextWaveIndex);
    }
}
