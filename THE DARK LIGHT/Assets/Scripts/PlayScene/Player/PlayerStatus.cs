using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerStatus : MonoBehaviour {
    public static PlayerStatus Instance;

    public int Level = 1;
    public int MaxHp = 100;
    public int MaxMp = 100;
    private int hp = 100;
    public int LevelUpExp = 100;
    public int Exp = 0;
    public int Hp {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            if (hp > MaxHp)
            {
                hp = MaxHp;
            }else if (hp < 0)
            {
                hp = 0;
            }
        }
    }
    private int mp = 100;
    public int Mp
    {
        get
        {
            return mp;
        }
        set
        {
            mp = value;
            if (mp > MaxMp)
            {
                mp = MaxMp;
            }
            else if (mp < 0)
            {
                mp = 0;
            }
        }
    }
    public int GoldCoin = 1000;

    public int Attack = 20;
    public int Attack_Plus = 0;
    public int Defence = 20;
    public int Defence_Plus = 0;
    public int Speed = 20;
    public int Speed_Plus = 0;

    public int PointPlus = 0;

    public ApplicationType playerType = ApplicationType.Magician;

    private void Start()
    {
        UISkillManager.Instance.UpdateCanUse(Level);
    }
    public void GetCoin(int coinCount)
    {
        GoldCoin += coinCount;
        UIBagManager.Instance.UpdateCoinText();
    }

    public bool ReduceCoin(int price,int number,int id)
    {
        int coinCount = price * number;
        if (coinCount <= GoldCoin)
        {
            GoldCoin -= coinCount;
            UIBagManager.Instance.UpdateCoinText();
            UIBagManager.Instance.GetItem(id, number);
        }
        else
        {
            return false;
        }
        return true;
    }

    public void GetAttack(int i = 1)
    {
        Attack_Plus+=i;
    }
    public void GetDefence(int i = 1)
    {
        Defence_Plus+=i;
    }
    public void GetSpeed(int i = 1)
    {
        Speed_Plus+=i;
    }
    public void ReduceAttack(int i = 1)
    {
        Attack_Plus -= i;
    }
    public void ReduceDefence(int i = 1)
    {
        Defence_Plus -= i;
    }
    public void ReduceSpeed(int i = 1)
    {
        Speed_Plus -= i;
    }
    public bool ReducePointPlus()
    {
        if (PointPlus > 0)
        {
            PointPlus--;
            if(PointPlus == 0)
            {
                return false;
            }
        }
        return true;
    }

    public void LevelUp(int UpLevel = 1)
    {
        Level += UpLevel;
        PointPlus += (5 * UpLevel);
        Attack += 5;
        Defence += 5;
        Speed += 5;
        UIStatus.Instacne.UpdatePointPlus();
        UIStatus.Instacne.UpdateOriginalProperties();
        UISkillManager.Instance.UpdateCanUse(Level);
        UIPlayerStatus.Instance.LevelUp();
        AddHpByLevelUp(UpLevel);
    }

    public void AddExp(int exp)
    {
        Exp += exp;
        if (Exp >= LevelUpExp)
        {
            LevelUp();
            Exp = Exp - LevelUpExp;
        }

        UIExpBarManager.Instance.UpdateSlider();
    }

    private void AddHpByLevelUp(int UpLevel = 1)
    {
        MaxHp += UpLevel * 100;
        Hp = MaxHp;
        UIPlayerStatus.Instance.HpUpByLevel();
    }

    public void AddHp(int hp)
    {
        Hp += hp;
        UIPlayerStatus.Instance.UpdateHp();
    }
    public void ReduceHp(int hp)
    {
        Hp -= hp;
        UIPlayerStatus.Instance.UpdateHp();
    }
    public void AddMp(int mp)
    {
        Mp += mp;
        UIPlayerStatus.Instance.UpdateMp();
    }
    public void ReduceMp(int mp)
    {
        Mp -= mp;
        UIPlayerStatus.Instance.UpdateMp();
    }
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddHp(10);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ReduceHp(10);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddMp(10);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            ReduceMp(10);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddExp(7);
        }
    }
}
