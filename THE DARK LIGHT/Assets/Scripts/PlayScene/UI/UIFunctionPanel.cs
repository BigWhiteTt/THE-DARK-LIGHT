using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionPanel : MonoBehaviour {
    public GameObject BagPanel;
    public GameObject StatusPanel;
    public GameObject EquipPanel;
    public GameObject SkillPanel;

    private Vector3 OriginalBagPanelPos;
    private Vector3 OriginalStatusPanel;
    private Vector3 OriginalEquipPanel;
    private Vector3 OriginalSkillPanel;
    private void Start()
    {
        OriginalBagPanelPos = BagPanel.transform.localPosition;
        BagPanel.transform.localPosition = new Vector3(10000, 10000, 10000);
        OriginalStatusPanel = StatusPanel.transform.localPosition;
        StatusPanel.transform.localPosition = new Vector3(10000, 10000, 10000);
        OriginalEquipPanel = EquipPanel.transform.localPosition;
        EquipPanel.transform.localPosition = new Vector3(10000, 10000, 10000);
        OriginalSkillPanel = SkillPanel.transform.localPosition;
        SkillPanel.transform.localPosition = new Vector3(10000, 10000, 10000);

    }
    public void OnStatusButtonClick()
    {
        if (StatusPanel.transform.localPosition != new Vector3(10000, 10000, 10000))
        {
            StatusPanel.transform.localPosition = new Vector3(10000, 10000, 10000);
        }
        else
        {
            StatusPanel.transform.localPosition = OriginalStatusPanel;
        }
    }
    public void OnBagButtonClick()
    {
        if (BagPanel.transform.localPosition != new Vector3(10000, 10000, 10000))
        {
            BagPanel.transform.localPosition = new Vector3(10000, 10000, 10000);
        }
        else
        {
            BagPanel.transform.localPosition = OriginalBagPanelPos;
        }
    }
    public void OnEquipButtonClick()
    {
        if (EquipPanel.transform.localPosition != new Vector3(10000, 10000, 10000))
        {
            EquipPanel.transform.localPosition = new Vector3(10000, 10000, 10000);
        }
        else
        {
            EquipPanel.transform.localPosition = OriginalEquipPanel;
        }
    }
    public void OnSkillButtonClick()
    {
        if (SkillPanel.transform.localPosition != new Vector3(10000, 10000, 10000))
        {
            SkillPanel.transform.localPosition = new Vector3(10000, 10000, 10000);
        }
        else
        {
            SkillPanel.transform.localPosition = OriginalSkillPanel;
        }
    }
    public void OnSettingButtonClick()
    {

    }
}
