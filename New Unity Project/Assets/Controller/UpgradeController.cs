using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    private int scraps = 0;
    public GameObject submarine;
    private SubmarineController subController;
    public int healthUpgradeCost = 50;
    public int armorUpgradeCost = 100;
    public int depthUpgradeCost = 75;
    public int speedUpgradeCost = 50;
    public int lightUpgradeCost = 60;
    public int repairCost = 50;
    private float baseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        subController = submarine.GetComponent<SubmarineController>();
        baseSpeed = subController.maxSpeed;
        if (Input.GetKeyDown(KeyCode.P)) Damage();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(scraps);
    }

    public void AddScrap(int amount)
	{
        scraps += amount;
	}
    public void UpgradeHealth() 
    {
        if (subController.maxHealth != 2000 && scraps >= healthUpgradeCost)
        {
            subController.maxHealth += 200;
            subController.currentHealth += 200;
            scraps -= healthUpgradeCost;
        }
    }
    public void UpgradeArmor() 
    {
        if (subController.armor != 300 && scraps >= armorUpgradeCost)
        {
            subController.armor += 25;
            scraps -= armorUpgradeCost;
        }
    }
    public void UpgradeDepth()
    {
        if (subController.maxDepth != 2800 && scraps >= depthUpgradeCost)
        {
            subController.maxDepth += 400;
            scraps -= depthUpgradeCost;
        }
    }
    public void UpgradeSpeed() 
    {
        if (subController.maxSpeed != (baseSpeed*1.5f) && scraps >= speedUpgradeCost)
        {
            subController.maxSpeed += baseSpeed/5f;
            scraps -= speedUpgradeCost;
        }
    }
    public void UpgradeLight() 
    {
        if (subController.lightPower < 5 && scraps >= lightUpgradeCost)
        {
            subController.lightPower += 1;
            scraps -= lightUpgradeCost;
        }
    }
    public void Repair()
    {
        if (subController.currentHealth < subController.maxHealth && scraps >= repairCost)
        {
            subController.currentHealth += 50;
            if (subController.currentHealth < subController.maxHealth)
            {
                subController.currentHealth = subController.maxHealth;
            }
            scraps -= repairCost;
        }
    }
    public void Damage()
    {
        subController.currentHealth -= 50;
    }
    public int getScrapCount()
    {
        return scraps;
    }

}
