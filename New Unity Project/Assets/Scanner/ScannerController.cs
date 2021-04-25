using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerController : MonoBehaviour
{
    public float minimumScanLength = 3f;
    
    private Dictionary<int, EntityInfo> entityDatabase = new Dictionary<int, EntityInfo>();

    private float startScanTimestamp;

    private bool scanning;

    private GameObject scanningObject = null;
    // Update is called once per frame

    public Dictionary<int, EntityInfo> getEntityDatabase()
    {
        return entityDatabase;
    }
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Entity"))
            {
                if (!scanning)
                {
                    startScanTimestamp = Time.time;
                    scanning = true;
                    scanningObject = hit.collider.gameObject;
                }
                else if (scanningObject != hit.collider.gameObject)
                {
                    Debug.Log("NEW OBJECT!");
                    startScanTimestamp = Time.time;
                    scanningObject = hit.collider.gameObject;
                }
                
                if (scanning && (Time.time > startScanTimestamp + minimumScanLength))
                {
                    var entityInfo = hit.collider.GetComponent<Entity>().getEntityInfo();
                    if (!entityDatabase.ContainsKey(entityInfo.id))
                    {
                        entityDatabase.Add(entityInfo.id, entityInfo);
                        Debug.Log("FOUND: " + entityInfo.id);
                    }
                }
                else
                {
                    Debug.Log(Time.time - startScanTimestamp);
                }
                
            }
            else
            {
                scanning = false;
                scanningObject = null;
            }
        }
        else
        {
            scanning = false;
            scanningObject = null;
        }
    }
}
