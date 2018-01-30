using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour {


    public static ObjManager Instance;
    public TextAsset objInfoText;
    public Dictionary<int, SingleObjInfo> objInfo = new Dictionary<int, SingleObjInfo>();

    private void Awake()
    {
        Instance = this;
        ReadInfo();
    }


    private void ReadInfo()
    {
        string text = objInfoText.text;
        string[] oneInfo = text.Split('\n');

        foreach(string str in oneInfo)
        {
            SingleObjInfo info = new SingleObjInfo();
            string[] properties = str.Split(',');
            int id =int.Parse(properties[0]);
            string name = properties[1];
            string icon_name = properties[2];
            string objType = properties[3];
            ObjType type = ObjType.Drug;
            switch (objType)
            {
                case "Drug":
                    type = ObjType.Drug;
                    break;
                case "Equip":
                    type = ObjType.Equip;
                    break;
                case "Material":
                    type = ObjType.Material;
                    break;
            }
            info.id = id; info.name = name; info.icon_name = icon_name; info.objType = type;
            if (type == ObjType.Drug)
            {
                int hp = int.Parse(properties[4]);
                int mp = int.Parse(properties[5]);
                int price_sell = int.Parse(properties[6]);
                int price_buy = int.Parse(properties[7]);
                info.hp = hp;info.mp = mp;info.price_sell = price_sell; info.price_buy = price_buy;
            }else if(type == ObjType.Equip)
            {
                info.attack = int.Parse(properties[4]);
                info.defence = int.Parse(properties[5]);
                info.speed = int.Parse(properties[6]);
                info.price_sell = int.Parse(properties[9]);
                info.price_buy = int.Parse(properties[10]);
                string equipType = properties[7];
                switch (equipType)
                {
                    case "Headgear":
                        info.equipType = EquipType.Headgear;
                        break;
                    case "Armor":
                        info.equipType = EquipType.Armor;
                        break;
                    case "RightHand":
                        info.equipType = EquipType.RightHand;
                        break;
                    case "LeftHand":
                        info.equipType = EquipType.LeftHand;
                        break;
                    case "Shoe":
                        info.equipType = EquipType.Shoe;
                        break;
                    case "Accessory":
                        info.equipType = EquipType.Accessory;
                        break;
                }
                string applicationType = properties[8];
                switch (applicationType)
                {
                    case "Magician":
                        info.applicationType = ApplicationType.Magician;
                        break;
                    case "Swordman":
                        info.applicationType = ApplicationType.Swordman;
                        break;
                    case "Common":
                        info.applicationType = ApplicationType.Common;
                        break;
                }
            }
            objInfo.Add(id, info);
        }
    }
    public SingleObjInfo GetObjectInfo(int id)
    {
        SingleObjInfo info;
        objInfo.TryGetValue(id, out info);
        return info;
    }
}

public enum ObjType
{
    Drug,
    Equip,
    Material

}

public enum EquipType
{
    Headgear,
    Armor,
    RightHand,
    LeftHand,
    Shoe,
    Accessory
}

public enum ApplicationType
{
    Swordman,
    Magician,
    Common
}
public class SingleObjInfo
{
    public int id;
    public string name;
    public string icon_name;
    public ObjType objType;
    public int hp;
    public int mp;
    public int price_sell;
    public int price_buy;


    public int attack;
    public int defence;
    public int speed;
    public EquipType equipType;
    public ApplicationType applicationType;
}

