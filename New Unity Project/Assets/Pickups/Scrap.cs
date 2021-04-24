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
		controller = GameObject.Find("UpgradeController").GetComponent<UpgradeController>();
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
