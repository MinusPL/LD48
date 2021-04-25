using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonController : MonoBehaviour
{
    private EntityInfo entityInfo;

    private ScannerMenu scannerMenu;
    // Start is called before the first frame update

    public void setEntityInfo(EntityInfo entityInfo)
    {
        this.entityInfo = entityInfo;
    }

    public void setScannerMenu(ScannerMenu scannerMenu)
    {
        this.scannerMenu = scannerMenu;
    }

    public void OnClick()
    {
        scannerMenu.SelectOption(entityInfo);
    }
}
