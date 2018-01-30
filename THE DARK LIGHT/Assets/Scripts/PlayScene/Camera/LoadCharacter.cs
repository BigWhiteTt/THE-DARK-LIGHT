using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour {
    public GameObject[] characterPref;
	// Use this for initialization
	void Start () {
        //LoadPlayer();
        InstanceSomeThing();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void LoadPlayer()
    {
        GameObject playerGO = Instantiate(characterPref[PlayerPrefs.GetInt("CharacterIndex")]);
    }

    void InstanceSomeThing()
    {
        UISkillManager.Instance.UpdateSkillItems();
    }
}
