using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    private Renderer rend;
    private Color startColor;
    [HideInInspector] public GameObject turret;
    [HideInInspector] public TurretBlueprint turretBlueprint;
    [HideInInspector] public bool isUpgraded = false;
    private Vector3 pos;
    private BuildManager BuildManager;
    private void Start()
    {
        BuildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (turret != null)
        {
            BuildManager.SelectNode(this);
            return;
        }

        if (!BuildManager.CanBuild)
            return;
        
        BuildTurret(BuildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        _turret.transform.Translate(new Vector3(0f,0.5f,0f));
        turret = _turret;
        turretBlueprint = blueprint;
        GameObject effect = Instantiate(BuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        effect.transform.Translate(new Vector3(0f,0.5f,0f));
        Destroy(effect,5f);
        Debug.Log("Turret built");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        // Get rid of old turret
        Destroy(turret);
        // Build new turret
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        _turret.transform.Translate(new Vector3(0f,0.5f,0f));
        turret = _turret;
        GameObject effect = Instantiate(BuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        effect.transform.Translate(new Vector3(0f,0.5f,0f));
        Destroy(effect,5f);
        
        isUpgraded = true;
        
        Debug.Log("Turret upgraded");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        
        GameObject effect = Instantiate(BuildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        effect.transform.Translate(new Vector3(0f,0.5f,0f));
        Destroy(effect,5f);
        
        Destroy(turret);
        turretBlueprint = null;
    }
    
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!BuildManager.CanBuild)
            return;
        if (BuildManager.HasMoney)
            rend.material.color = hoverColor;
        else
            rend.material.color = notEnoughMoneyColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
