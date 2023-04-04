using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public GameObject buildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI;
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

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        
        selectedNode = node;
        turretToBuild = null;
        
        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
    
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
