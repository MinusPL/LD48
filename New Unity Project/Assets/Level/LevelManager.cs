using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Randomly generated pickups
    public GameObject scrapPrefab;

    //Randomly generated entities

    //Required stuff (player handle etc)
    public GameObject player;

    //variables to regulate spawn rates etc.

    // Start is called before the first frame update
    public float startDistanceForScrap = 10.0f;
    public float endDistanceForScrap = 50.0f;
    public float chanceForScrap = 20.0f;

    public float timeForSpawn = 0.1f;
    private float spawnerTimer = 0.0f;
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
        }
    }
    public void SpawnScrap()
	{
        Vector2 newLocation = Random.insideUnitCircle * endDistanceForScrap;

        if (Vector3.Distance(player.transform.position, new Vector3(newLocation.x, newLocation.y, 0.0f)) < startDistanceForScrap) return;
        var obj = Instantiate(scrapPrefab);
        obj.transform.position = new Vector3(newLocation.x, newLocation.y, 0.0f);
    }
}
