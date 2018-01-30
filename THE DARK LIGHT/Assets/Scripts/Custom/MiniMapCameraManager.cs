using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraManager : MonoBehaviour {
    private Transform Player;
    private Vector3 LastPosition;
    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        LastPosition = Player.position - this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Player.position - LastPosition;
    }

    public void ZoomIn()
    {
        if (transform.GetComponent<Camera>().orthographicSize < 10)
        {
            transform.GetComponent<Camera>().orthographicSize += 1;
        }
    }

    public void ZoomOut()
    {
        if (transform.GetComponent<Camera>().orthographicSize > 1)
        {
            transform.GetComponent<Camera>().orthographicSize -= 1;
        }
        
    }
}
