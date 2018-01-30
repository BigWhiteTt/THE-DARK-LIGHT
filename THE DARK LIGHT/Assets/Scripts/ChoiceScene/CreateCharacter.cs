using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateCharacter : MonoBehaviour {

    public GameObject[] characterPRE;
    public InputField PlayerName;
    private List<GameObject> characteraGO = new List<GameObject>();
    private int Index = 0;
	// Use this for initialization
	void Start () {
        InstanceCharacter();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void  InstanceCharacter()
    {
        for (int i = 0; i < characterPRE.Length; i++)
        {
            GameObject character = Instantiate(characterPRE[i]) as GameObject;
            character.transform.parent = this.transform;
            characteraGO.Add(character);
            character.SetActive(false);
        }
        characteraGO[Index].SetActive(true);
    }

    public void RightChoice()
    {
        Index++;
        if (Index == characteraGO.Count)
        {
            characteraGO[characteraGO.Count - 1].SetActive(false);
            Index = 0;
        }
        else
        {
            characteraGO[Index - 1].SetActive(false);
        }
        characteraGO[Index].SetActive(true);
    }

    public void LeftChoice()
    {
        Index--;
        if (Index == -1)
        {
            characteraGO[0].SetActive(false);
            Index = characteraGO.Count-1;
        }
        else
        {
            characteraGO[Index + 1].SetActive(false);
        }
        characteraGO[Index].SetActive(true);
    }

    public void OnOKChilck()
    {
        PlayerPrefs.SetString("PlayerName", PlayerName.text);
        PlayerPrefs.SetInt("CharacterIndex", Index);

        Invoke("LoadScene", 1f);

    }

    void LoadScene()
    {
        SceneManager.LoadScene("PlayScene");
    }
}

