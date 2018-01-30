using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class SkillItem : MonoBehaviour,IPointerClickHandler{

    public Image icon;
    public Text skillName;
    public Text describe;
    public Text needNum;
    public Image isCanUse;

    private SkillInfo info;


    public void UpdateItem(SkillInfo info)
    {
        icon.sprite =  Resources.Load<Sprite>(@"RPG\GUI\Icon\" + info.icon_name);
        skillName.text = info.name;
        describe.text = info.describe;
        needNum.text = info.mp.ToString();
        this.info = info;
    }

    public void CanUse()
    {
        isCanUse.gameObject.SetActive(false);
    }

    public void CannotUse()
    {
        isCanUse.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left && !isCanUse.gameObject.activeSelf)
        {
            TempItemManager.Instance.isFollow = true;
            TempItemManager.Instance.UpdateSkill(this.info);
        }
    }       
     
}
