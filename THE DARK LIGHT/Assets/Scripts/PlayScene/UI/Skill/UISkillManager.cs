using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillManager : MonoBehaviour {
    public static UISkillManager Instance;

    public List<SkillItem> skillItems = new List<SkillItem>();

    private void Awake()
    {
        Instance = this;
    }

  

    public void UpdateSkillItems()
    {
        int i = 0;
        foreach (SkillInfo info in SkillManager.Instance.skillInfo.Values)
        {      
            if(info.applicationType == PlayerStatus.Instance.playerType)
            {
                skillItems[i].UpdateItem(info);
                i++;
            }  
        }
    }

    public void UpdateCanUse(int level)
    {
        for(int i = 0; i < level; i++)
        {
            if (i < skillItems.Count)
            {
                skillItems[i].CanUse();
            }
        }
    }
}
