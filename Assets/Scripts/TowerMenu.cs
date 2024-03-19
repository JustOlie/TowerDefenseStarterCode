using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    public static TowerMenu instance;

    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;

    private VisualElement root;

    private ConstructionSite selectedSite;

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
        root = GetComponent<UIDocument>().rootVisualElement;

        archerButton = root.Q<Button>("archer-button");
        swordButton = root.Q<Button>("sword-button");
        wizardButton = root.Q<Button>("wizard-button");
        updateButton = root.Q<Button>("upgrade-button");
        destroyButton = root.Q<Button>("delete-button");

        if (archerButton != null)
        {
            archerButton.clicked += OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked += OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked += OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked += OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked += OnDestroyButtonClicked;
        }

        root.visible = false;
    }

    public void ToggleVisibility()
    {
        root.visible = !root.visible;
    }

    public void SetSite(ConstructionSite site)
    {
        selectedSite = site;
        EvaluateMenu();
    }

    private void EvaluateMenu()
    {
        if (selectedSite == null)
            return;

        // Check site level
        int siteLevel = (int)selectedSite.Level;

        switch (siteLevel)
        {
            case 0:
                updateButton.SetEnabled(false);
                destroyButton.SetEnabled(false);
                break;
            case 1:
            case 2:
                archerButton.SetEnabled(false);
                wizardButton.SetEnabled(false);
                swordButton.SetEnabled(false);
                destroyButton.SetEnabled(true);
                break;
            case 3:
                archerButton.SetEnabled(false);
                wizardButton.SetEnabled(false);
                swordButton.SetEnabled(false);
                updateButton.SetEnabled(false);
                destroyButton.SetEnabled(true);
                break;
            default:
                Debug.LogError("Unknown site level: " + siteLevel);
                break;
        }
    }


    private void OnArcherButtonClicked()
    {
        // Handle archer button click
    }

    private void OnSwordButtonClicked()
    {
        // Handle sword button click
    }

    private void OnWizardButtonClicked()
    {
        // Handle wizard button click
    }

    private void OnUpdateButtonClicked()
    {
        // Handle update button click
    }

    private void OnDestroyButtonClicked()
    {
        // Handle destroy button click
    }

    private void OnDestroy()
    {
        if (archerButton != null)
        {
            archerButton.clicked -= OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked -= OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked -= OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked -= OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked -= OnArcherButtonClicked;
        }
    }
}
