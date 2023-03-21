using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    public GameObject buildEffect;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one build manager in scene!");
            return;
        }
        instance = this;
    }
    


    public bool CanBuild => turretToBuild != null;
    public bool HasMoney => PlayerStats.Money >= turretToBuild.cost;

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        turret.transform.Translate(new Vector3(0f,0.5f,0f));
        node.turret = turret;
        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        effect.transform.Translate(new Vector3(0f,0.5f,0f));
        Destroy(effect,5f);
        Debug.Log("Turret built, Money left:" + PlayerStats.Money);
    }
}
