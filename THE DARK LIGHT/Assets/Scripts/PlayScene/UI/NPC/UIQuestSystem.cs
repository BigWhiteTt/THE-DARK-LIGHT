using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIQuestSystem : UIManager {
    public GameObject questPanel;
    public int KillCount = 0;
    private int TargetCount = 10;
    private Transform Player;
    private Vector3 OriginalQuesePanelPos;

    private void Start()
    {
        OriginalQuesePanelPos = questPanel.transform.position;
        questPanel.transform.position = new Vector3(10000, 10000, 10000);
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowUI(questPanel,OriginalQuesePanelPos);
            questPanel.transform.Find("Schedule").GetComponent<Text>().text = "您已经击杀" + KillCount + "/" + TargetCount + "只小狼";
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

    public void Cancel()
    {
        HideUI(questPanel);
    }

    public void Accept()
    {
        questPanel.transform.Find("Quset").gameObject.SetActive(false);
        questPanel.transform.Find("Accept").gameObject.SetActive(false);
        questPanel.transform.Find("Cancel").gameObject.SetActive(false);
        questPanel.transform.Find("Schedule").gameObject.SetActive(true);
        questPanel.transform.Find("Schedule").GetComponent<Text>().text = "您已经击杀"+0+"/"+TargetCount+"只小狼";
        questPanel.transform.Find("OK").gameObject.SetActive(true);
    }

    public void QuestFinish()
    {
        if (KillCount >= TargetCount)
        {
            Player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
            Player.GetComponent<PlayerStatus>().GetCoin(1000);
            KillCount = 0;
            questPanel.transform.Find("Schedule").GetComponent<Text>().text = "您已经击杀" + KillCount + "/" + TargetCount + "只小狼";
        }
        else
        {
            HideUI(questPanel);
        }
    }
}
