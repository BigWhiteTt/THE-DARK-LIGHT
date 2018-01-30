using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIPlayerStatus : MonoBehaviour {
    public static UIPlayerStatus Instance;

    public Slider HpSlider;
    public Slider MpSlider;
    public Text Level;
    public Text Name;
    public Text MaxHp;
    public Text CurHp;
    public Text MaxMp;
    public Text CurMp;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InstanceNewPlayer();
    }

    private void InstanceNewPlayer()
    {
        HpSlider.value = 1;
        MpSlider.value = 1;
        Level.text = PlayerStatus.Instance.Level.ToString();
        Name.text = PlayerPrefs.GetString("PlayerName");
        MaxHp.text = PlayerStatus.Instance.MaxHp.ToString();
        CurHp.text = PlayerStatus.Instance.Hp.ToString();
        MaxMp.text = PlayerStatus.Instance.MaxMp.ToString();
        CurMp.text = PlayerStatus.Instance.Mp.ToString();
    }

    public void LevelUp()
    {
        Level.text = PlayerStatus.Instance.Level.ToString();
    }

    public void HpUpByLevel()
    {
        MaxHp.text = PlayerStatus.Instance.MaxHp.ToString();
        CurHp.text = MaxHp.text;
        HpSlider.value = 1;
    }

    public void UpdateHp()
    {
        CurHp.text = PlayerStatus.Instance.Hp.ToString();
        HpSlider.value = PlayerStatus.Instance.Hp /(float) PlayerStatus.Instance.MaxHp;
    }

    public void UpdateMp()
    {
        CurMp.text = PlayerStatus.Instance.Mp.ToString();
        MpSlider.value = PlayerStatus.Instance.Mp / (float)PlayerStatus.Instance.MaxMp;
    }

}
