using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSystem : UIManager {

    public GameObject shopPanel;
    private Vector3 OriginalShopPanelPos;
    public GameObject InputDialogs;
    public Text BuyNumText;
    private int Id;
    public GameObject Hint;

    private void Start()
    {
        OriginalShopPanelPos = shopPanel.transform.position;
        shopPanel.transform.position = new Vector3(10000, 10000, 10000);
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowUI(shopPanel, OriginalShopPanelPos);           
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

    public void OnBuyHpButtonClick()
    {
        InputDialogs.SetActive(true);
        Id = 1002;
    }
    public void OnBuyhpButtonClick()
    {
        InputDialogs.SetActive(true);
        Id = 1001;
    }
    public void OnBuyMpButtonClick()
    {
        InputDialogs.SetActive(true);
        Id = 1003;
    }

    public void OnOKClick()
    {
        int number = System.Int32.Parse(BuyNumText.text);
        if (number > 0)
        {
            SingleObjInfo info = ObjManager.Instance.GetObjectInfo(Id);
            if(!PlayerStatus.Instance.ReduceCoin(info.price_buy,number,Id))
            {
                ShowHint();
                Invoke("HideHintAndDialogs", 2);
            }
        }
        InputDialogs.SetActive(false);
    }
    private void ShowHint()
    {
        Hint.SetActive(true);
    }
    private void HideHintAndDialogs()
    {
        Hint.SetActive(false);
        InputDialogs.SetActive(false);
    }
}
