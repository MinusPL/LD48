using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Randomly generated pickups


    //Randomly generated entities

    //Required stuff (player handle etc)
    public GameObject player;

    //variables to regulate spawn rates etc.

    // Start is called before the first frame update
    public GameObject scrapPrefab;
    public float startDistanceForScrap = 10.0f;
    public float endDistanceForScrap = 50.0f;
    public float chanceForScrap = 20.0f;

    //Shark
    public GameObject sharkPrefab;
    public float startDistanceForShark = 20.0f;
    public float endDistanceForShark = 80.0f;
    public float chanceForShark = 10.0f;


    //Other values
    public float timeForSpawn = 0.1f;
	private float spawnerTimer = 0.0f;
    //public GameObject foreground;
    //float maxV = 0.26f;
    //float minV = 0.07f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnerTimer += Time.deltaTime;
        if (spawnerTimer >= timeForSpawn)
        {
            spawnerTimer = 0.0f;
            //Scrap generator
            float chance = Random.Range(0.0f, 100.0f);
            if (chance <= chanceForScrap)
            {
                SpawnScrap();
            }

            chance = Random.Range(0.0f, 100.0f);
            if (chance <= chanceForShark)
            {
                SpawnShark();
            }
        }

        //UpdateFog();
    }

    /*public void UpdateFog()
	{
        var fogPlane = foreground.GetComponent<Renderer>();

        float h, s, v;
        var color = fogPlane.material.GetColor("_EmissionColor");
        Color.RGBToHSV(color, out h, out s, out v);
        Debug.Log($"{v}");
        float depth = Mathf.Abs(player.transform.position.y);
        float dN = Mathf.InverseLerp(150, 0, depth);
        float nI = Mathf.Lerp(minV, maxV, dN);
        fogPlane.material.SetColor("_EmissionColor", Color.HSVToRGB(h,s,nI));
    }*/

    public void SpawnScrap()
	{
        Vector2 newLocation = Random.insideUnitCircle * endDistanceForScrap;

        if (Vector3.Distance(player.transform.position, new Vector3(newLocation.x, newLocation.y, 0.0f)) < startDistanceForScrap) return;
        var obj = Instantiate(scrapPrefab);
        obj.transform.position = new Vector3(newLocation.x, newLocation.y);
    }

    public void SpawnShark()
	{
        Vector2 newLocation = Random.insideUnitCircle * endDistanceForShark;
        if (Vector3.Distance(player.transform.position, new Vector3(newLocation.x, newLocation.y, 0.0f)) < startDistanceForShark) return;
        var obj = Instantiate(sharkPrefab);
        obj.transform.position = new Vector3(newLocation.x, newLocation.y);
        float side = Vector3.Dot(obj.transform.position, player.transform.position);
        obj.GetComponent<Shark>().dir = side < 0 ? (short)-1 : (short)1;
    }
}
