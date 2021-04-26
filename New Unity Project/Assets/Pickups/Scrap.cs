using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Scrap : MonoBehaviour
{
	private UpgradeController controller;

	private int amount = 0;
	private float lastAction = 0;
	private Vector3 goTo = Vector3.zero;
	private float maxTime = 0;
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
		if (Time.time - lastAction < maxTime)
		{
			transform.position += goTo * Time.deltaTime;
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		Debug.Log("DUPA!!!!!!!");
		if (other.CompareTag("Player"))
		{
			controller.AddScrap(amount);
			Destroy(gameObject);
		}
		else if (other.gameObject.layer == LayerMask.NameToLayer("Dolphin"))
		{
			Debug.Log("DUPA2!!!!!!!");
			maxTime = Random.Range(3, 10);
			lastAction = Time.time;
			goTo = new Vector3(other.gameObject.GetComponent<Dolphin>().dir * Random.Range(0.5f, 2), Random.Range(-2f, 2f), 0);
		}
	}
	
}
