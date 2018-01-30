using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {
    public float MoveSpeed;
    public float MoveTo;
    public Image StartImage;
    public Image TitleImage;
    public Image HintImage;
    public float DisappearSpeed;
    public Button StartButton;
    public Button LoadButton;

    private Color cor;
    private Color tempCor;
    private bool isInputAnyKey = false;

	// Use this for initialization
	void Start () {
        cor = StartImage.color;
        tempCor = HintImage.color;
        isInputAnyKey = false;
    }
	
	// Update is called once per frame
	void Update () {
        MoveCamera();

        ControlDisappear();
    }
    void ControlDisappear()
    {
        if (Mathf.Abs(StartImage.color.a - 0f) >= 0.01)
        {
            cor.a = Mathf.Lerp(cor.a, 0f, Time.deltaTime * DisappearSpeed);
            StartImage.color = cor;
        }
        else
        {
            StartImage.transform.gameObject.SetActive(false);
        }
    }

    void MoveCamera()
    {
        if (Mathf.Abs(this.transform.position.z - MoveTo) >= 0.1)
        {
            //this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y
            //,Mathf.Lerp(this.transform.position.z, -20f, Time.deltaTime * Speed));
            transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
        }
        else
        {
            StartMenu();
            HideImageAndShowButton();
        }
    }

    void StartMenu()
    {
        if (Mathf.Abs(TitleImage.color.a - 1f) >= 0.05)
        {
            tempCor.a = Mathf.Lerp(tempCor.a, 1f, Time.deltaTime * 20);
            TitleImage.color = tempCor;
            HintImage.color = tempCor;
        }
        else
        {
            tempCor.a = Mathf.PingPong(Time.time, 1f);
            HintImage.color = tempCor;

        }
    }

    void HideImageAndShowButton()
    {
        if (isInputAnyKey == false)
        {
            if (Input.anyKey)
            {
                isInputAnyKey = true;
                HintImage.gameObject.SetActive(false);
                StartButton.gameObject.SetActive(true);
                LoadButton.gameObject.SetActive(true);
            }
        }
    }

    public void OnStartButtonClick()
    {
        Invoke("LoadScene", 1f);
        
    }

    void LoadScene()
    {
        SceneManager.LoadScene("ChoiceScene");
    }

    public void OnLoadButtonClick()
    {
        
    }
}
