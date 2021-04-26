using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public GameObject player;
    public AudioLowPassFilter filter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Abs(player.transform.position.y);
        
        float dN = Mathf.InverseLerp(50, 1300, y);
        float nI = Mathf.Lerp(22000, 400, dN);
        filter.cutoffFrequency = nI;
    }
}
