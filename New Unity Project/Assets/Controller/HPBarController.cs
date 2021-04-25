using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    public GameObject submarine;
    private SubmarineController subController;
    private static Image hpBar;
    private float hpBarFill;
    void Start()
    {
        subController = submarine.GetComponent<SubmarineController>();
        hpBar = GetComponent<Image>();
    }

    void Update()
    {
        hpBarFill = subController.currentHealth / subController.maxHealth;
        hpBar.fillAmount = hpBarFill;
        if(hpBarFill>0.4f)
        {
            hpBar.color = Color.green;
        }
        else if (hpBarFill <= 0.4f && hpBarFill > 0.2f)
        {
            hpBar.color = Color.yellow;
        }
        else if (hpBarFill <= 0.2f)
        {
            hpBar.color = Color.red;
        }
    }
}
