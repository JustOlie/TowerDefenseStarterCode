using UnityEngine;

using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    public static TowerMenu instance;

    private Button archerbutton;
    private Button swordbutton;
    private Button wizardbutton;
    private Button upgradebutton;
    private Button destroybutton;

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

        archerbutton = root.Q<Button>("archer-button");
        swordbutton = root.Q<Button>("sword-button");
        wizardbutton = root.Q<Button>("wizard-button");
        upgradebutton = root.Q<Button>("upgrade-button");
        destroybutton = root.Q<Button>("delete-button");

        if (archerbutton != null)
        {
            archerbutton.clicked += OnArcherButtonClicked;
        }

        if (swordbutton != null)
        {
            swordbutton.clicked += OnSwordButtonClicked;
        }

        if (wizardbutton != null)
        {
            wizardbutton.clicked += OnWizardButtonClicked;
        }

        if (upgradebutton != null)
        {
            upgradebutton.clicked += OnUpdateButtonClicked;
        }

        if (destroybutton != null)
        {
            destroybutton.clicked += OnDestroyButtonClicked;
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
                upgradebutton.SetEnabled(false);
                destroybutton.SetEnabled(false);
                break;
            case 1:
            case 2:
                archerbutton.SetEnabled(false);
                wizardbutton.SetEnabled(false);
                swordbutton.SetEnabled(false);
                destroybutton.SetEnabled(true);
                break;
            case 3:
                archerbutton.SetEnabled(false);
                wizardbutton.SetEnabled(false);
                swordbutton.SetEnabled(false);
                upgradebutton.SetEnabled(false);
                destroybutton.SetEnabled(true);
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
        if (archerbutton != null)
        {
            archerbutton.clicked -= OnArcherButtonClicked;
        }

        if (swordbutton != null)
        {
            swordbutton.clicked -= OnSwordButtonClicked;
        }

        if (wizardbutton != null)
        {
            wizardbutton.clicked -= OnWizardButtonClicked;
        }

        if (upgradebutton != null)
        {
            upgradebutton.clicked -= OnUpdateButtonClicked;
        }

        if (destroybutton != null)
        {
            destroybutton.clicked -= OnArcherButtonClicked;
        }
    }
}
