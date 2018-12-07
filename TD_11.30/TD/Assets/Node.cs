using UnityEngine.EventSystems;
using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color NotenoughMonyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBulePrint TurretBulePrint;
    [HideInInspector]
    public bool isUpgrade = false;

    private Renderer rend;
    private Color startColor;

    BuildManager BuildManager;

     void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        BuildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

     void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!BuildManager.CanBuild)
        {
            return;
           
        }

        if (turret != null)
        {
            BuildManager.SelectNode(this);
            return;
        }

        BuildTurret(BuildManager.GetTurretToBuild());

    }

    void BuildTurret(TurretBulePrint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("not enough money to build that!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        TurretBulePrint = blueprint;

        GameObject effect = (GameObject)Instantiate(BuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);


    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < TurretBulePrint.upgradeCost)
        {
            Debug.Log("not enough money to upgrade that!");
            return;
        }

        PlayerStats.Money -= TurretBulePrint.upgradeCost;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(TurretBulePrint.upgradedprefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(BuildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgrade = true;

        Debug.Log("Upgraded!!!");
    }

    public void SellTurret()
    {
        PlayerStats.Money += TurretBulePrint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(BuildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        TurretBulePrint = null;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!BuildManager.CanBuild)
        {
            return;
        }
        if (BuildManager.HasMoney)
        {

            rend.material.color = hoverColor;
        }
        else {

            rend.material.color = NotenoughMonyColor;
        }
        
    }

     void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
