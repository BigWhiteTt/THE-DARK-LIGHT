using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour {
    public static UIStatus Instacne;
    private void Awake()
    {
        Instacne = this;
    }

    public Text Attack;
    public Text Defence;
    public Text Speed;
    public Text Point;
    public Text AttackCount;
    public Text DefenceCount;
    public Text SpeedCount;
    public GameObject AddButton;

    public void AddAttackClick()
    {
        AddAttack();
        UpdatePointPlus();
        UpdateAttackCount();
    }
    public void AddDefenceClick()
    {
        AddDefence();
        UpdatePointPlus();
        UpdateDefenceCount();
    }
    public void AddSpeedClick()
    {
        AddSpeed();
        UpdatePointPlus();
        UpdateSpeedCount();
    }

    private void AddAttack()
    {
        PlayerStatus.Instance.GetAttack();
        if (!PlayerStatus.Instance.ReducePointPlus())
        {
            HideButton();
        }
        Attack.text = "伤害 = " + PlayerStatus.Instance.Attack + " + " + PlayerStatus.Instance.Attack_Plus;
    }
    private void AddDefence()
    {
        PlayerStatus.Instance.GetDefence();
        if (!PlayerStatus.Instance.ReducePointPlus())
        {
            HideButton();
        }
        Defence.text = "防御 = " + PlayerStatus.Instance.Defence + " + " + PlayerStatus.Instance.Defence_Plus;
    }
    private void AddSpeed()
    {
        PlayerStatus.Instance.GetSpeed();
        if (!PlayerStatus.Instance.ReducePointPlus())
        {
            HideButton();
        }
        Speed.text = "速度 = " + PlayerStatus.Instance.Speed + " + " + PlayerStatus.Instance.Speed_Plus;
    }

    public void UpdatePointPlus()
    {
        Point.text = "剩余点数：" + PlayerStatus.Instance.PointPlus;
        if (PlayerStatus.Instance.PointPlus > 0)
        {
            ShowButton();
        }
    }
    public void UpdateOriginalProperties()
    {
        Attack.text = "伤害 = " + PlayerStatus.Instance.Attack + " + " + PlayerStatus.Instance.Attack_Plus;
        Defence.text = "防御 = " + PlayerStatus.Instance.Defence + " + " + PlayerStatus.Instance.Defence_Plus;
        Speed.text = "速度 = " + PlayerStatus.Instance.Speed + " + " + PlayerStatus.Instance.Speed_Plus;
        UpdateAttackCount();
        UpdateDefenceCount();
        UpdateSpeedCount();
    }
    private void UpdateAttackCount()
    {
        AttackCount.text = "伤害：" + (PlayerStatus.Instance.Attack + PlayerStatus.Instance.Attack_Plus);
    }
    private void UpdateDefenceCount()
    {
        DefenceCount.text = "防御：" + (PlayerStatus.Instance.Defence + PlayerStatus.Instance.Defence_Plus);
    }
    private void UpdateSpeedCount()
    {
        SpeedCount.text = "速度：" + (PlayerStatus.Instance.Speed + PlayerStatus.Instance.Speed_Plus);
    }
    private void HideButton()
    {
        AddButton.SetActive(false);
    }

    private void ShowButton()
    {
        AddButton.SetActive(true);
    }

}
