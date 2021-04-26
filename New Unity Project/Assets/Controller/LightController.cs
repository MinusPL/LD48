using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject lightC;
    public GameObject submarine;

    public float maxLightIntensity = 0.8f;
    public float minLightDepth = 20.0f;

    public float minLightIntensity = 0.0f;
    public float maxLightDepth = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float depth = Mathf.Abs(submarine.transform.position.y);
        float dN = Mathf.InverseLerp(minLightDepth, maxLightDepth, depth);
        float nI = Mathf.Lerp(minLightIntensity, maxLightIntensity, dN);
        lightC.GetComponent<Light>().intensity = nI;
    }
}
