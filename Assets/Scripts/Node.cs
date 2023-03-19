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
    [Header("Optional")]
    public GameObject turret;
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
        if (!BuildManager.CanBuild)
            return;
        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen");
            return;
        }

        BuildManager.BuildTurretOn(this);

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
