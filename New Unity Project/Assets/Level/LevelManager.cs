using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Randomly generated pickups


    //Randomly generated entities

    //Required stuff (player handle etc)
    public GameObject player;
    public int helenaCounter = 5;

    //variables to regulate spawn rates etc.

    // Start is called before the first frame update
    public GameObject scrapPrefab;
    public float startDistanceForScrap = 10.0f;
    public float endDistanceForScrap = 50.0f;
    public float chanceForScrap = 20.0f;

    //Shark
    public GameObject sharkPrefab;
    public float startDistanceForShark = 10.0f;
    public float endDistanceForShark = 80.0f;
    public float chanceForShark = 10.0f;

    //Angler
    public GameObject anglerPrefab;
    public float startDistanceForAngler = 10.0f;
    public float endDistanceForAngler = 40.0f;
    public float maxChanceForAngler = 30.0f;
    public float minDepthForAngler = 1000.0f;
    public float maxDepthForAngler = 1300.0f;

    //Other values
    public float timeForSpawn = 0.5f;
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
            float playerDepth = Mathf.Abs(player.transform.position.y);
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

            if(player.transform.position.y < -990.0f)
			{
                float anglerChance;
                if (playerDepth > minDepthForAngler)
                    anglerChance = ((playerDepth - minDepthForAngler) / (maxDepthForAngler - minDepthForAngler)) * maxChanceForAngler;
                else
                    anglerChance = 0.01f;

                if(chance <= anglerChance)
				{
                    SpawnAngler();
				}
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
        Vector2 newCoordinates = Random.insideUnitCircle * endDistanceForScrap;
        Vector3 newLocation = player.transform.position + new Vector3(newCoordinates.y, newCoordinates.y);
        if (Vector3.Distance(player.transform.position, newLocation) < startDistanceForScrap) return;
        var obj = Instantiate(scrapPrefab);
        obj.transform.position = new Vector3(newLocation.x, newLocation.y);
    }

    public void SpawnShark()
	{
        Vector2 newCoordinates = Random.insideUnitCircle * endDistanceForShark;
        Vector3 newLocation = player.transform.position + new Vector3(newCoordinates.y, newCoordinates.y);
        if (Vector3.Distance(player.transform.position, newLocation) < startDistanceForShark) return;
        var obj = Instantiate(sharkPrefab);
        obj.transform.position = new Vector3(newLocation.x, newLocation.y);
        float side = Vector3.Dot(obj.transform.position, player.transform.position);
        obj.GetComponent<Shark>().dir = side < 0 ? (short)-1 : (short)1;
    }

    public void SpawnAngler()
	{
        int depth;
        Vector2 newCoordinates = Random.insideUnitCircle * endDistanceForAngler;
        Vector3 newLocation = player.transform.position + new Vector3(newCoordinates.y, newCoordinates.y);
        int i = Random.Range(1, 99);
        if (i < 34) depth = 1;
        else if (i < 67) depth = 0;
        else depth = -1;

        float z = 0.0f;
        switch(depth)
		{
            case 1:
                z = Random.Range(1.5f, 6.0f);
                break;
            case 0:
                z = 0;
                break;
            case -1:
                z = -1.5f;
                break;
        }

        if (Vector3.Distance(player.transform.position, new Vector3(newLocation.x, newLocation.y, z)) < startDistanceForAngler) return;
        var obj = Instantiate(anglerPrefab);
        obj.transform.position = new Vector3(newLocation.x, newLocation.y);
        obj.transform.rotation = Random.Range(0, 100) % 2 == 0 ? Quaternion.Euler(0.0f, 0.0f, 0.0f) : Quaternion.Euler(0.0f, 180.0f, 0.0f);
        obj.GetComponent<AnglerFish>().levelManager = this;
    }
}
