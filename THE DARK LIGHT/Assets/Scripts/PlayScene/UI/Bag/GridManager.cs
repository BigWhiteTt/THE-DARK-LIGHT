using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    public GameObject itemPrefab;
    public AudioSource audio;
    public int id;
    public int number;
    private Text numText;
    private void Start()
    {
        numText = transform.GetChild(0).GetComponent<Text>();
        number = 0;
    }

    public void GetItem(int id, int num = 1)
    {
        if (transform.childCount == 1)
        {
            SingleObjInfo info = ObjManager.Instance.GetObjectInfo(id);
            GameObject item = Instantiate(itemPrefab);
            item.transform.SetParent(this.transform);
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<ItemManager>().id = id;
            item.GetComponent<ItemManager>().UpdateIcon(info);
            this.id = id;
            this.number+=num;
            numText.text = number.ToString();
            numText.gameObject.SetActive(true);
            return;
        }
        //if (transform.childCount > 1)
        //{
        //    if (id == transform.GetChild(1).GetComponent<ItemManager>().id)
        //    {
        //        number+=num;
        //        numText.text = number.ToString();
        //        numText.gameObject.SetActive(true);
        //    }
        //    return;
        //}
    }

    public void UpdateNumberText()
    {
        numText.text = number.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (TempItemManager.Instance.isFollow == false && transform.childCount > 1)
            {
                audio.Play();
                TempItemManager.Instance.isFollow = true;
                TempItemManager.Instance.LastClickedGrid = this.gameObject;
                TempItemManager.Instance.UpdateIcon(transform.GetChild(1).GetComponent<Image>().sprite, this.number, this.id);
                Destroy(transform.GetChild(1).gameObject);
                numText.gameObject.SetActive(false);
                return;
            }
            if (TempItemManager.Instance.isFollow == true && transform.childCount == 1)
            {
                audio.Play();
                TempItemManager.Instance.isFollow = false;
                TempItemManager.Instance.ResetPos();
                GameObject item = Instantiate(itemPrefab);
                item.transform.SetParent(this.transform);
                item.transform.localPosition = Vector3.zero;
                item.GetComponent<Image>().sprite = TempItemManager.Instance.GetComponent<Image>().sprite;
                this.number = TempItemManager.Instance.number;
                numText.text = number.ToString();
                numText.gameObject.SetActive(true);
                this.id = TempItemManager.Instance.id;
                return;
            }
            if (TempItemManager.Instance.isFollow == true && transform.childCount > 1)
            {
                audio.Play();
                TempItemManager.Instance.isFollow = false;
                TempItemManager.Instance.ResetPos();
                Sprite tempSprite = transform.GetChild(1).GetComponent<Image>().sprite;
                int tempNum = this.number;
                transform.GetChild(1).GetComponent<Image>().sprite = TempItemManager.Instance.GetComponent<Image>().sprite;
                int tempId = this.id;
                this.number = TempItemManager.Instance.number;
                this.id = TempItemManager.Instance.id;
                numText.text = number.ToString();
                numText.gameObject.SetActive(true);

                GameObject item = Instantiate(itemPrefab);
                item.transform.SetParent(TempItemManager.Instance.LastClickedGrid.transform);
                item.transform.localPosition = Vector3.zero;
                item.GetComponent<Image>().sprite = tempSprite;
                TempItemManager.Instance.LastClickedGrid.transform.GetComponent<GridManager>().number = tempNum;
                TempItemManager.Instance.LastClickedGrid.transform.GetComponent<GridManager>().id = tempId;
                TempItemManager.Instance.LastClickedGrid.transform.GetComponent<GridManager>().numText.text = TempItemManager.Instance.LastClickedGrid.transform.GetComponent<GridManager>().number.ToString();
                TempItemManager.Instance.LastClickedGrid.transform.GetComponent<GridManager>().numText.gameObject.SetActive(true);
                return;
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            SingleObjInfo info = ObjManager.Instance.GetObjectInfo(id);
            if (info.objType == ObjType.Equip)
            {
                if (info.applicationType == PlayerStatus.Instance.playerType || info.applicationType == ApplicationType.Common)
                {
                    if (transform.childCount > 1)
                    {
                        UIEquipPanel.Instance.GetEquip(info);
                        ReduceItem();
                    }
                }
            }
            else if (info.objType == ObjType.Drug)
            {
                if (transform.childCount > 1)
                {
                    if (info.id == 1001 || info.id == 1002)
                    {
                        PlayerStatus.Instance.AddHp(info.hp);
                    }
                    else if (info.id == 1003)
                    {
                        PlayerStatus.Instance.AddMp(info.mp);
                    }
                    ReduceItem();
                }
            }
        }
    }

    private void ReduceItem()
    {
        number--;
        numText.text = number.ToString();
        if (number == 0)
        {
            Destroy(this.transform.GetChild(1).gameObject);
            numText.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TempItemManager.Instance.isFollow == false && this.transform.childCount > 1)
        {
            SingleObjInfo info = ObjManager.Instance.GetObjectInfo(id);
            if (info != null)
            {
                UpdateInfo.Instance.UpdateInfos(info);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UpdateInfo.Instance.ResetPos();
    }
}