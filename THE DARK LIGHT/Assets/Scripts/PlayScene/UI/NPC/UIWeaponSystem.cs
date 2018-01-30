using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponSystem : UIManager {
    public GameObject weaponPanel;
    private Vector3 OriginalWeaponPanelPos;

    public List<UIWeaponShopItem> weaponItems = new List<UIWeaponShopItem>();


    // Use this for initialization
    void Start () {
        OriginalWeaponPanelPos = weaponPanel.transform.position;
        weaponPanel.transform.position = new Vector3(10000, 10000, 10000);
        ShowWeaponItem();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void ShowWeaponItem()
    {
        int i = 0;
        foreach (SingleObjInfo info in ObjManager.Instance.objInfo.Values)
        {
            if(info.objType == ObjType.Equip)
            {
                weaponItems[i].UpdateItem(info);
                i++;
            }
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowUI(weaponPanel, OriginalWeaponPanelPos);
        }
    }

    private void OnMouseEnter()
    {
        CursorManager.Instance.SetTalk();
    }
    private void OnMouseExit()
    {
        CursorManager.Instance.SetNormal();
    }
}
