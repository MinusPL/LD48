using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgMenuScrapCounter : MonoBehaviour
{
    public UpgradeController upgController;
    Text scrapCounter; 
    void Start()
    {
        scrapCounter = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scrapCounter.text = "Current scrap: " + upgController.getScrapCount();
    }
}