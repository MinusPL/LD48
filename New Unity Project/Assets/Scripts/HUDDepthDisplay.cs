using UnityEngine;
using UnityEngine.UI;

public class HUDDepthDisplay : MonoBehaviour
{
    public GameObject submarine;
    private float depth;
    Text depthText;
    void Start()
    {
        depthText = GetComponent<Text>();
    }

    void Update()
    {
        depth = Mathf.Floor(submarine.transform.position.y * 2);
        depthText.text = "Current depth: " + -depth + "m";
    }
}
