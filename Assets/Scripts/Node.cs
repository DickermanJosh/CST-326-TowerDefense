using System;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private GameObject turret;
    private Vector3 pos;
    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        //pos = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z);
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: Display on screen");
            return;
        }
        // Build a turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);
        turret.transform.Translate(new UnityEngine.Vector3(0f,0.5f,0f));
    }

    private void OnMouseEnter()
    {
       rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
