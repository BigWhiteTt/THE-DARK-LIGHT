using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour {
    public static SkillManager Instance;
    public TextAsset skillInfoText;
    public Dictionary<int, SkillInfo> skillInfo = new Dictionary<int, SkillInfo>();
    private void Awake()
    {
        Instance = this;
        ReadInfo();

        //foreach(SkillInfo info in skillInfo.Values)
        //{

        //    string str = info.id + "," + info.name + "," + info.icon_name + "," + info.describe + "," + info.skillType
        //        + "," + info.applyProperty + "," + info.applyValue + "," + info.applyTime + "," + info.mp + "," + info.coldTime
        //        + "," + info.applicationType + "," + info.level + "," + info.releaseType+","+ info.releaseDistance;
        //    Debug.LogError(str);
        //}
    }

    private void ReadInfo()
    {
        string text = skillInfoText.text;
        string[] oneInfo = text.Split('\n');

        foreach (string str in oneInfo)
        {
            SkillInfo info = new SkillInfo();
            string[] properties = str.Split(',');
            info.id =int.Parse(properties[0]);
            info.name = properties[1];
            info.icon_name = properties[2];
            info.describe = properties[3];
            string skillType = properties[4];
            SkillType type = SkillType.Buff;
            switch (skillType)
            {
                case "Passive":
                    type = SkillType.Passive;
                    break;
                case "Buff":
                    type = SkillType.Buff;
                    break;
                case "SingleTarget":
                    type = SkillType.SingleTarget;
                    break;
                case "MultiTarget":
                    type = SkillType.MultiTarget;
                    break;
            }
            info.skillType = type;

            string applyProperty = properties[5];
            ApplyProperty property = ApplyProperty.Attack;
            switch (applyProperty)
            {
                case "Attack":
                    property = ApplyProperty.Attack;
                    break;
                case "Def":
                    property = ApplyProperty.Def;
                    break;
                case "Speed":
                    property = ApplyProperty.Speed;
                    break;
                case "AttackSpeed":
                    property = ApplyProperty.AttackSpeed;
                    break;
                case "HP":
                    property = ApplyProperty.HP;
                    break;
                case "MP":
                    property = ApplyProperty.MP;
                    break;
            }
            info.applyProperty = property;

            info.applyValue =int.Parse(properties[6]);
            info.applyTime = int.Parse(properties[7]);
            info.mp = int.Parse(properties[8]);
            info.coldTime = int.Parse(properties[9]);

            string applicationType = properties[10];
            ApplicationType application = ApplicationType.Magician;
            switch (applicationType)
            {
                case "Swordman":
                    application = ApplicationType.Swordman;
                    break;
                case "Magician":
                    application = ApplicationType.Magician;
                    break;
            }
            info.applicationType = application;

            info.level = int.Parse(properties[11]);

            string releaseType = properties[12];
            ReleaseType release = ReleaseType.Self;
            switch (releaseType)
            {
                case "Self":
                    release = ReleaseType.Self;
                    break;
                case "Enemy":
                    release = ReleaseType.Enemy;
                    break;
                case "Position":
                    release = ReleaseType.Position;
                    break;
            }
            info.releaseType = release;

            info.releaseDistance = float.Parse(properties[13]);

            info.effectName = properties[14];
            info.aniName = properties[15];
            info.aniTime = float.Parse(properties[16]);
            skillInfo.Add(info.id, info);
        }
    }


}

//public enum SkillApplicationType
//{
//    Swordman,
//    Magician
//}

public enum SkillType
{
    Passive,
    Buff,
    SingleTarget,
    MultiTarget
}

public enum ApplyProperty
{
    Attack,
    Def,
    Speed,
    AttackSpeed,
    HP,
    MP
}

public enum ReleaseType
{
    Self,
    Enemy,
    Position
}

public class SkillInfo
{
    public int id;
    public string name;
    public string icon_name;
    public string describe;
    public SkillType skillType;
    public ApplyProperty applyProperty;
    public int applyValue;
    public int applyTime;
    public int mp;
    public int coldTime;
    public ApplicationType applicationType;
    public int level;
    public ReleaseType releaseType;
    public float releaseDistance;
    public string effectName;
    public string aniName;
    public float aniTime = 0;
}
