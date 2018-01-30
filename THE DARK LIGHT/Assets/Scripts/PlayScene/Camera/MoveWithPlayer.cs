using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithPlayer : MonoBehaviour {
    private Transform Player;
    private Vector3 LastPosition;
    private bool isRotate;
    private Vector3 OriginalPos;
    private Vector3 OriginalRos;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag(Tags.Player).transform ;
        OriginalPos = this.transform.position;
        OriginalRos = this.transform.eulerAngles;
        LastPosition = Player.position-this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = Player.position - LastPosition;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 100)
                Camera.main.fieldOfView += 2;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView >= 40)
                Camera.main.fieldOfView -= 2;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isRotate = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotate = false;
        }
        if (isRotate)
        {
            OriginalPos = this.transform.position;
            OriginalRos = this.transform.eulerAngles;
            if (Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Mouse Y")))
            {
                transform.RotateAround(Player.position, Vector3.up, 3 * Input.GetAxis("Mouse X"));
                if (transform.eulerAngles.y > 240 || transform.eulerAngles.y < 120)
                {
                    transform.position = OriginalPos;
                    transform.eulerAngles = OriginalRos;
                }

            }
            if (Mathf.Abs(Input.GetAxis("Mouse X")) < Mathf.Abs(Input.GetAxis("Mouse Y")))
            {
                transform.RotateAround(Player.position, Vector3.left, 3 * Input.GetAxis("Mouse Y"));
                if (transform.eulerAngles.x > 70 || transform.eulerAngles.x < 30)
                {
                    transform.position = OriginalPos;
                    transform.eulerAngles = OriginalRos;
                }
               
            }
            Vector3 tempV3 = transform.eulerAngles;
            tempV3.z = 0;
            transform.eulerAngles = tempV3;
            
        }
        LastPosition = Player.position - this.transform.position;
    }
}
