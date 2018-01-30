using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour/*,IBeginDragHandler,IEndDragHandler,IDragHandler*/
{
    //public GameObject coin;
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    SetDraggedPosition(eventData);
    //    this.transform.parent = coin.transform;
    //    transform.GetComponent<Image>().raycastTarget = false;
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    SetDraggedPosition(eventData);
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    SetDraggedPosition(eventData);
    //    transform.GetComponent<Image>().raycastTarget = true;
    //}
    //private void SetDraggedPosition(PointerEventData eventData)
    //{
    //    var rt = gameObject.GetComponent<RectTransform>();
    //    Vector3 globalMousePos;
    //    if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))
    //    {
    //        rt.position = globalMousePos;
    //    }
    //}
    public int id = 0;
    public void UpdateIcon(SingleObjInfo info)
    {
        this.GetComponent<Image>().sprite = Resources.Load<Sprite>(@"RPG\GUI\Icon\" + info.icon_name);
        //Texture2D texture = (Texture2D)Resources.Load(@"RPG\GUI\Icon\" + info.icon_name);
        //GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

}
