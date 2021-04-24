using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UMenuTexts : MonoBehaviour
{
    public GameObject submarine;
    private SubmarineController subController;
    Text statValues;
    void Start()
    {
        subController = submarine.GetComponent<SubmarineController>();
        statValues = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        statValues.text = subController.maxHealth + "\r\n" + subController.armor + "/" + Mathf.Round(100f-(100f/(subController.armor+100f))*100f) +"%\r\n" + subController.maxDepth + "m\r\n" + subController.maxSpeed + "m/s\r\n" + subController.lightPower + "m\r\n";
    }
}
