using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    public static TowerMenu instance;

    private Button archerbutton;
    private Button swordbutton;
    private Button wizardbutton;
    private Button updatebutton;
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

        archerbutton = root.Q<Button>("archerbutton");
        swordbutton = root.Q<Button>("swordbutton");
        wizardbutton = root.Q<Button>("wizardbutton");
        updatebutton = root.Q<Button>("upgradebutton");
        destroybutton = root.Q<Button>("deletebutton");

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

        if (updatebutton != null)
        {
            updatebutton.clicked += OnUpdateButtonClicked;
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
        root.visible = selectedSite != null;

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
                archerbutton.SetEnabled(true);
                wizardbutton.SetEnabled(true);
                swordbutton.SetEnabled(true);
                updatebutton.SetEnabled(false);
                destroybutton.SetEnabled(false);
                break;
            case 1:
            case 2:
                archerbutton.SetEnabled(false);
                wizardbutton.SetEnabled(false);
                swordbutton.SetEnabled(false);
                updatebutton.SetEnabled(true);
                destroybutton.SetEnabled(true);
                break;
            case 3:
                archerbutton.SetEnabled(false);
                wizardbutton.SetEnabled(false);
                swordbutton.SetEnabled(false);
                updatebutton.SetEnabled(false);
                destroybutton.SetEnabled(true);
                break;
            default:
                Debug.LogError("Unknown site level: " + siteLevel);
                break;
        }
    }

    private void OnArcherButtonClicked()
    {
        GameManager.instance.Build(PathEnum.Towers.Archer, PathEnum.SiteLevel.level1);
    }

    private void OnSwordButtonClicked()
    {
        GameManager.instance.Build(PathEnum.Towers.Sword, PathEnum.SiteLevel.level1);
    }

    private void OnWizardButtonClicked()
    {
        GameManager.instance.Build(PathEnum.Towers.Wizard, PathEnum.SiteLevel.level1);
    }

    private void OnUpdateButtonClicked()
    {
        if (selectedSite == null)
            return;

        // Check if the selected site has a tower type
        if (selectedSite.TowerType == null)
        {
            Debug.LogWarning("Cannot upgrade site because no tower has been built.");
            return;
        }

        // Increase the level of the selected site by one
        PathEnum.SiteLevel newLevel = selectedSite.Level + 1;

        // Update the site with the new level
        GameManager.instance.Build(selectedSite.TowerType.Value, newLevel);

        // Update menu evaluation after upgrading
        EvaluateMenu();
    }

    private void OnDestroyButtonClicked()
    {
        if (selectedSite == null)
            return;

        // Destroy the tower on the selected site by setting its level to 0
        selectedSite.SetTower(null, PathEnum.SiteLevel.level0, PathEnum.Towers.None);

        // Update menu evaluation after destroying the tower
        EvaluateMenu();
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

        if (updatebutton != null)
        {
            updatebutton.clicked -= OnUpdateButtonClicked;
        }

        if (destroybutton != null)
        {
            destroybutton.clicked -= OnDestroyButtonClicked;
        }
    }
}
