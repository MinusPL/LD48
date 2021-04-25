using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScannerMenu : MonoBehaviour
{
    public RectTransform contentObject;
    public GameObject buttonPrefab;

    public RectTransform entityInfoRect;
    public TMP_Text entityTitle;
    public TMP_Text entityDescription;
    public RawImage entityTexture;
    public ScannerController scannerController;

    private EntityInfo selectedOption = null;

    public void OpenMenu(IDictionary<int, EntityInfo> entityInfos)
    {
        if (selectedOption == null)
        {
            entityInfoRect.gameObject.SetActive(false);
        }
        else
        {
            entityInfoRect.gameObject.SetActive(true);
        }
        
        foreach (Transform child in contentObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        foreach (var entityInfo in entityInfos.Values)
        {
            var obj = Instantiate(buttonPrefab, contentObject.transform);
            Button button = obj.GetComponent<Button>();
            button.GetComponentInChildren<Text>().text = entityInfo.name;
            button.GetComponent<ButtonController>().setEntityInfo(entityInfo);
            button.GetComponent<ButtonController>().setScannerMenu(this);
        }
    }

    public void SelectOption(EntityInfo entityInfo)
    {
        selectedOption = entityInfo;
        entityInfoRect.gameObject.SetActive(true);
        entityTitle.text = entityInfo.name;
        entityTexture.texture = entityInfo.texture;
        entityDescription.text = entityInfo.description;
    }
}
