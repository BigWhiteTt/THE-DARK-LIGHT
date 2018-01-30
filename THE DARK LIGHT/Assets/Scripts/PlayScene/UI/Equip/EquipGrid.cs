using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipGrid : MonoBehaviour {
    public GameObject EquipItemPre;
	public void GetEquip(SingleObjInfo info)
    {
        GameObject equipItem = Instantiate(EquipItemPre);
        equipItem.transform.SetParent(this.transform);
        equipItem.GetComponent<Image>().sprite = Resources.Load<Sprite>(@"RPG\GUI\Icon\" + info.icon_name);
        equipItem.GetComponent<EquipItem>().info = info;
        equipItem.transform.localPosition = Vector3.zero;
        AddProperties(info);
    }


    public void AddProperties(SingleObjInfo info)
    {
        EquipType equipType = info.equipType;
        switch (equipType)
        {
            case EquipType.Headgear:
                PlayerStatus.Instance.GetAttack(info.attack);
                PlayerStatus.Instance.GetDefence(info.defence);
                PlayerStatus.Instance.GetSpeed(info.speed);
                break;
            case EquipType.Armor:
                PlayerStatus.Instance.GetAttack(info.attack);
                PlayerStatus.Instance.GetDefence(info.defence);
                PlayerStatus.Instance.GetSpeed(info.speed);
                break;
            case EquipType.RightHand:
                PlayerStatus.Instance.GetAttack(info.attack);
                PlayerStatus.Instance.GetDefence(info.defence);
                PlayerStatus.Instance.GetSpeed(info.speed);
                break;
            case EquipType.LeftHand:
                PlayerStatus.Instance.GetAttack(info.attack);
                PlayerStatus.Instance.GetDefence(info.defence);
                PlayerStatus.Instance.GetSpeed(info.speed);
                break;
            case EquipType.Shoe:
                PlayerStatus.Instance.GetAttack(info.attack);
                PlayerStatus.Instance.GetDefence(info.defence);
                PlayerStatus.Instance.GetSpeed(info.speed);
                break;
            case EquipType.Accessory:
                PlayerStatus.Instance.GetAttack(info.attack);
                PlayerStatus.Instance.GetDefence(info.defence);
                PlayerStatus.Instance.GetSpeed(info.speed);
                break;
        }
        UIStatus.Instacne.UpdateOriginalProperties();
    }

    public void ReduceProperties(SingleObjInfo info)
    {
        EquipType equipType = info.equipType;
        switch (equipType)
        {
            case EquipType.Headgear:
                PlayerStatus.Instance.ReduceAttack(info.attack);
                PlayerStatus.Instance.ReduceDefence(info.defence);
                PlayerStatus.Instance.ReduceSpeed(info.speed);
                break;
            case EquipType.Armor:
                PlayerStatus.Instance.ReduceAttack(info.attack);
                PlayerStatus.Instance.ReduceDefence(info.defence);
                PlayerStatus.Instance.ReduceSpeed(info.speed);
                break;
            case EquipType.RightHand:
                PlayerStatus.Instance.ReduceAttack(info.attack);
                PlayerStatus.Instance.ReduceDefence(info.defence);
                PlayerStatus.Instance.ReduceSpeed(info.speed);
                break;
            case EquipType.LeftHand:
                PlayerStatus.Instance.ReduceAttack(info.attack);
                PlayerStatus.Instance.ReduceDefence(info.defence);
                PlayerStatus.Instance.ReduceSpeed(info.speed);
                break;
            case EquipType.Shoe:
                PlayerStatus.Instance.ReduceAttack(info.attack);
                PlayerStatus.Instance.ReduceDefence(info.defence);
                PlayerStatus.Instance.ReduceSpeed(info.speed);
                break;
            case EquipType.Accessory:
                PlayerStatus.Instance.ReduceAttack(info.attack);
                PlayerStatus.Instance.ReduceDefence(info.defence);
                PlayerStatus.Instance.ReduceSpeed(info.speed);
                break;
        }
        UIStatus.Instacne.UpdateOriginalProperties();
    }
}
