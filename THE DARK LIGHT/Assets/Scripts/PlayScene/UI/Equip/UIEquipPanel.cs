using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipPanel : MonoBehaviour {
    public static UIEquipPanel Instance;

    public EquipGrid Headgear;
    public EquipGrid Armor;
    public EquipGrid RightHand;
    public EquipGrid LeftHand;
    public EquipGrid Shoe;
    public EquipGrid Accessory;
    public void Awake()
    {
        Instance = this;
    }

    public void GetEquip(SingleObjInfo info)
    {
        if (info.applicationType == PlayerStatus.Instance.playerType || info.applicationType == ApplicationType.Common)
        {
            EquipType equipType = info.equipType;
            switch (equipType)
            {
                case EquipType.Headgear:
                    if (Headgear.transform.childCount == 0)
                    {
                        Headgear.GetEquip(info);
                    }
                    else
                    {
                        ReplaceEquip(info, Headgear);
                    }
                    break;
                case EquipType.Armor:
                    if (Armor.transform.childCount == 0)
                    {
                        Armor.GetEquip(info);
                    }
                    else
                    {
                        ReplaceEquip(info, Armor);
                    }
                    break;
                case EquipType.RightHand:
                    if (RightHand.transform.childCount == 0)
                    {
                        RightHand.GetEquip(info);
                    }
                    else
                    {
                        ReplaceEquip(info, RightHand);
                    }
                    break;
                case EquipType.LeftHand:
                    if (LeftHand.transform.childCount == 0)
                    {
                        LeftHand.GetEquip(info);
                    }
                    else
                    {
                        ReplaceEquip(info, LeftHand);
                    }
                    break;
                case EquipType.Shoe:
                    if (Shoe.transform.childCount == 0)
                    {
                        Shoe.GetEquip(info);
                    }
                    else
                    {
                        ReplaceEquip(info, Shoe);
                    }
                    break;
                case EquipType.Accessory:
                    if (Accessory.transform.childCount == 0)
                    {
                        Accessory.GetEquip(info);
                    }
                    else
                    {
                        ReplaceEquip(info, Accessory);
                    }
                    break;
            }
        }
    }


    public void ReplaceEquip(SingleObjInfo info, EquipGrid equipGrid)
    {
        SingleObjInfo tempInfo = equipGrid.transform.GetChild(0).GetComponent<EquipItem>().info;
        equipGrid.ReduceProperties(tempInfo);
        Destroy(equipGrid.transform.GetChild(0).gameObject);
        equipGrid.GetEquip(info);
        UIBagManager.Instance.GetItem(tempInfo.id);
    }
}

