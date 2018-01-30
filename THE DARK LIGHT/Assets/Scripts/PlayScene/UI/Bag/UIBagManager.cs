using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBagManager : MonoBehaviour {
    public static UIBagManager Instance;

    public List<GridManager> girds = new List<GridManager>();
    public Text coinCountText;

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
    public void UpdateCoinText()
    {
        coinCountText.text = PlayerStatus.Instance.GoldCoin.ToString();
    }
    public void GetItem(int id,int num=1)
    {
        foreach(GridManager gird in girds)
        {
            if (gird.transform.childCount == 1)
            {
                gird.GetItem(id,num);
                break;
            }else if(gird.transform.childCount > 1)
            {
                if(id == gird.id)
                {
                    gird.number+=num;
                    gird.UpdateNumberText();
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetItem(Random.Range(1001, 1004));
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GetItem(Random.Range(2001, 2023));
        }

    }
}
