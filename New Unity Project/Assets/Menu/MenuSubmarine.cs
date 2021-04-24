using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSubmarine : MonoBehaviour
{
    private int correctKeys = 0;
    private bool turnOnEasterEgg = false;
    public float speedOfRotation = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!turnOnEasterEgg)
            transform.Rotate(0, speedOfRotation * Time.deltaTime, 0);
        else
        {
            if(transform.rotation.eulerAngles.y < 120 || transform.rotation.eulerAngles.y > 130)
                transform.Rotate(0, speedOfRotation * 10 * Time.deltaTime, 0);
            else
            {
                Debug.Log("Odłamkowym ładuj!");
            }
        }
        if (Input.anyKeyDown)
        {
            switch (correctKeys)
            {
                case 0:
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
                case 1:
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
                case 2:
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
                case 3:
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
                case 4:
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
                case 5:
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
                case 6:
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
                case 7:
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
                case 8:
                    if (Input.GetKeyDown(KeyCode.B))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
                case 9:
                    if (Input.GetKeyDown(KeyCode.A))
                        correctKeys += 1;
                    else correctKeys = 0;
                    break;
            }

            if (correctKeys >= 10 && !turnOnEasterEgg)
            {
                turnOnEasterEgg = true;
            }
        }
        else
        {
            //correctKeys = 0;
        }
    }
}
