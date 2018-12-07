using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBulePrint StandardTurret;
    public TurretBulePrint RocketLaucherTurret;
    public TurretBulePrint LaserTurret;

    BuildManager buildManager;

     void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(StandardTurret);
    }

    public void SelectRocketLaucherTurret()
    {
        Debug.Log("Rocket Lancher Turret Selected");
        buildManager.SelectTurretToBuild(RocketLaucherTurret);
    }

    public void SelectLaserTurret()
    {
        Debug.Log("Laser Turret Selected");
        buildManager.SelectTurretToBuild(LaserTurret);
    }
}
