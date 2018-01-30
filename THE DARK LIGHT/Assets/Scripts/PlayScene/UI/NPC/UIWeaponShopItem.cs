using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponShopItem : MonoBehaviour
{

    public Image Icon;
    public Text Name;
    public Text Describe;
    public Text Price;
    public GameObject Hint;
    private SingleObjInfo info;

    public void UpdateItem(SingleObjInfo info)
    {
        this.info = info;
        Icon.sprite = Resources.Load<Sprite>(@"RPG\GUI\Icon\" + info.icon_name);
        Name.text = info.name;
        Price.text = info.price_buy.ToString();
        if (info.objType == ObjType.Equip)
        {
            Describe.text = "+" + info.defence + "Defence";
            switch (info.equipType)
            {
                case EquipType.Shoe:
                    Describe.text = "+" + info.speed + "Speed";
                    break;
                case EquipType.RightHand:
                    Describe.text = "+" + info.attack + "Attack";
                    break;
            }
        }
    }

    public void OnBuyClick()
    {
        if (!PlayerStatus.Instance.ReduceCoin(info.price_buy, 1, info.id))
        {
            ShowHint();
            Invoke("HideHintAndDialogs", 2);
        }
    }

    private void ShowHint()
    {
        Hint.SetActive(true);
    }
    private void HideHintAndDialogs()
    {
        Hint.SetActive(false);
    }
}
