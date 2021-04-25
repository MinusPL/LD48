using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgMenuUpgCost : MonoBehaviour
{
    public UpgradeController upgController;
    Text upgCost;
    void Start()
    {
        upgCost = GetComponent<Text>();
        upgCost.text = "Upgrade cost\r\n" + upgController.healthUpgradeCost + "\r\n" + upgController.armorUpgradeCost + "\r\n" + upgController.depthUpgradeCost + "\r\n" + upgController.speedUpgradeCost + "\r\n" + upgController.lightUpgradeCost + "\r\n" + upgController.repairCost;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

