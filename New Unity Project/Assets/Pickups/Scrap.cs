using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
	private UpgradeController controller;

	private int amount = 0;
	public void Start()
	{
		amount = Random.Range(20, 35);
		float x = Random.Range(-15.0f, 15.0f);
		float y = Random.Range(-20.0f, 20.0f);
		float z = Random.Range(-180.0f, 180.0f);
		transform.rotation = Quaternion.Euler(x, y, z);
		controller = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
	}

	public void Update()
	{
		if (Vector3.Distance(transform.position, Camera.main.transform.position) > 100.0f) Destroy(gameObject);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			controller.AddScrap(amount);
			Destroy(gameObject);
		}
	}
	
}
