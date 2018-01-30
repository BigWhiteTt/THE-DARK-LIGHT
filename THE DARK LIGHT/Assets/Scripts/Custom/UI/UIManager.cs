using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public void HideUI(GameObject go)
    {
        go.transform.position = new Vector3(10000, 10000, 10000);
    }
     
     public void ShowUI(GameObject go, Vector3 OriginalQuesePanelPos)
    {
        go.transform.position = OriginalQuesePanelPos;
    }
}


