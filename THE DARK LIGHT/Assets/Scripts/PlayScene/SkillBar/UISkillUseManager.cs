using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillUseManager : MonoBehaviour {

    public List<UISkillItem> skillItems = new List<UISkillItem>();

    public void Update()
    {
        SkillListener();
    }

    public void SkillListener()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            UseSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSkill(2);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            UseSkill(3);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            UseSkill(4);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            UseSkill(5);
        }
    }

    public void UseSkill(int id)
    {
        if (skillItems[id].skillInfo != null && skillItems[id].CanUse)
        {
            skillItems[id].UseSkill();
            //Debug.LogError(skillItems[id].skillInfo.name);
            //Debug.LogError(skillItems[id].skillInfo.coldTime);
        }
    }
}
