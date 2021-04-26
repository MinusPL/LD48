using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(GoToMainMenu), 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            GoToMainMenu();
        }
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
