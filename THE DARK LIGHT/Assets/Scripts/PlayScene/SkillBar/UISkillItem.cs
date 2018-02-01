using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkillItem : MonoBehaviour, IPointerClickHandler
{
    public Image Icon;
    public Text Key;
    public SkillInfo skillInfo;
    public Image ColdMask;
    public bool CanUse;
    private float timer;

    private void Start()
    {
        timer = 0;
        CanUse = true;
    }

    private void Update()
    {
        if(timer != 0)
        {
            CanUse = false;
            timer -= Time.deltaTime;
            if(timer - 0 < 0.01)
            {
                timer = 0;
            }
            ColdMask.fillAmount = timer/ skillInfo.coldTime;
        }
        else
        {
            CanUse = true;
        }
    }

    public void UseSkill()
    {
        bool canUse = PlayerStatus.Instance.ReduceMp(skillInfo.mp);
        if (canUse)
        {
            PlayerAttack.Instance.UseSkill(skillInfo);
            timer = skillInfo.coldTime;
            ColdMask.fillAmount = 1;
        }
        else
        {
            Debug.LogError("MP 不足！");
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && CanUse)
        {
            UpdateIcon();
        }
        else
        {
            TempItemManager.Instance.isFollow = false;
            TempItemManager.Instance.ResetPos();
        }
    }


    public void UpdateIcon()
    {
        if (TempItemManager.Instance.skillInfo != null)
        {
            Icon.gameObject.SetActive(true);
            Key.gameObject.SetActive(true);
            Icon.sprite = TempItemManager.Instance.GetComponent<Image>().sprite;
            this.skillInfo = TempItemManager.Instance.skillInfo;
            TempItemManager.Instance.isFollow = false;
            TempItemManager.Instance.ResetPos();
        }
    }

   
}
