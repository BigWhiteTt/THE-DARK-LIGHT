using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClose : MonoBehaviour {
    public GameObject uiPanel;

	public void OnCloseClick()
    {
        uiPanel.transform.localPosition = new Vector3(10000, 10000, 10000);
    }
}
