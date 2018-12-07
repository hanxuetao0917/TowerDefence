using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBulePrint {

    public GameObject prefab;
    public int cost;

    public GameObject upgradedprefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
