using UnityEngine;

public class ConstructionSite
{
    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public PathEnum.SiteLevel Level { get; private set; }
    public PathEnum.Towers? TowerType { get; private set; }

    private GameObject tower;

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        TilePosition = tilePosition;
        // Pas de wereldpositie aan
        WorldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.5f, worldPosition.z);
        TowerType = null;
    }

    public void SetTower(GameObject newTower, PathEnum.SiteLevel newLevel, PathEnum.Towers newType)
    {
        // Controleer of er al een toren is
        if (tower != null)
        {
            // Verwijder de bestaande toren voordat je de nieuwe toewijst
            GameObject.Destroy(tower);
        }

        // Wijs de nieuwe toren en eigenschappen toe
        tower = newTower;
        Level = newLevel;
        TowerType = newType;
    }
}