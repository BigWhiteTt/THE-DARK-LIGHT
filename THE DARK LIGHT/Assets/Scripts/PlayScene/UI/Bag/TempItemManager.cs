using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempItemManager : MonoBehaviour {
    public static TempItemManager Instance;
    public GameObject LastClickedGrid;
    public Canvas canvas;
    // Use this for initialization
    public bool isFollow = false;
    public int number;
    public Text text;
    public int id;
    public SkillInfo skillInfo;
    private Vector3 OriginalPos;
    private void Awake()
    {
        Instance = this;
    }
    void Start () {
        OriginalPos = this.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        if (isFollow)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);
            transform.localPosition = position;
        }
    }
    public void ResetPos()
    {
        this.transform.position = OriginalPos;
    }
    public void UpdateIcon(Sprite sprite,int num,int id)
    {
        text.gameObject.SetActive(true);
        GetComponent<Image>().sprite = sprite;
        number = num;
        text.text = number.ToString();
        this.id = id;
        this.skillInfo = null;
    }

    public void UpdateSkill(SkillInfo skillInfo)
    {
        GetComponent<Image>().sprite = Resources.Load<Sprite>(@"RPG\GUI\Icon\" + skillInfo.icon_name);
        text.gameObject.SetActive(false);
        this.skillInfo = skillInfo;
    }
}
