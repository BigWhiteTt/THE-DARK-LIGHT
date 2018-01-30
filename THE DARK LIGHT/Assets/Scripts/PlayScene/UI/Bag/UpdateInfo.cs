using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInfo : MonoBehaviour {
    public static UpdateInfo Instance;
    public Text nameText;
    public Text addText;
    public Text sellPriceText;
    public Text buyPriceText;
    public Canvas canvas;
    public Text applicationText;
    private Vector3 OriginalPos;
    // Use this for initialization
    void Start () {
        Instance = this;
        OriginalPos = this.transform.position;
	}


    public void UpdateInfos(SingleObjInfo info)
    {
        UpdatePos();
        nameText.text = "名称:" + info.name;
        if(info.objType == ObjType.Drug)
        {
            if (info.id % 1000 == 3)
            {
                addText.text = "+Mp:" + info.mp;
            }
            else
            {
                addText.text = "+Hp:" + info.hp;
            }
            applicationText.text = "职业：通用";
        }
        else if(info.objType == ObjType.Equip){
            if(info.equipType == EquipType.Headgear)
            {
                addText.text = "+Defence:" + info.defence;
            }
            else if(info.equipType == EquipType.Armor)
            {
                addText.text = "+Defence:" + info.defence;
            }
            else if (info.equipType == EquipType.RightHand)
            {
                addText.text = "+Attack:" + info.attack;
            }
            else if (info.equipType == EquipType.LeftHand)
            {
                addText.text = "+Defence:" + info.defence;
            }
            else if (info.equipType == EquipType.Shoe)
            {
                addText.text = "+Speed:" + info.speed;
            }
            else if (info.equipType == EquipType.Accessory)
            {
                addText.text = "+Defence:" + info.defence;
            }
            if (info.applicationType == ApplicationType.Common)
            {
                applicationText.text = "职业：通用";
            }
            else if (info.applicationType == ApplicationType.Magician)
            {
                applicationText.text = "职业：魔法师";
            }
            else if (info.applicationType == ApplicationType.Swordman)
            {
                applicationText.text = "职业：战士";
            }
        }
       
        sellPriceText.text = "出售价:" + info.price_sell;
        buyPriceText.text = "购买价:" + info.price_buy;
    }

    public void UpdatePos()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
        transform.localPosition = position;
    }

    public void ResetPos()
    {
        transform.position = OriginalPos;
    }
}
