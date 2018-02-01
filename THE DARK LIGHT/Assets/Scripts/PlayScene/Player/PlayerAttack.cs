using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackState
{
    ControlWalk,
    NormalAttack,
    SkillAttack
}

public enum InAttack
{
    Move,
    Idel,
    Attack
}

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance;

    public AttackState state = AttackState.ControlWalk;

    public string normalAttackName = "Attack1";
    public string normalAttackIdleName = "Idle";
    private string currentAnimName;
    public float time_normalAttack = 0.8f;
    public float attackRate = 0.5f;
    public float timer = 0;
    public float min_distance = 5;
    public int attack = 40;
    public InAttack attackState = InAttack.Idel;
    private Transform attackTarget;
    private PlayerController control;
    private Animation animation1;
    private bool ShowEffect = false;

    public GameObject[] effectArray;
    private Dictionary<string, GameObject> effectDir = new Dictionary<string, GameObject>();

    public bool isLock = false;
    private SkillInfo info;
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start()
    {
        control = GetComponent<PlayerController>();
        animation1 = GetComponent<Animation>();
        currentAnimName = normalAttackName;
        foreach (GameObject go in effectArray)
        {
            effectDir.Add(go.name, go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        NormalAttack();

        SkillAttack();

    }

    private void NormalAttack()
    {
        if (isLock == false && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);
            if (isCollider && hitInfo.collider.tag == Tags.Enemy)
            {
                timer = 0;
                ShowEffect = false;
                attackTarget = hitInfo.transform;
                state = AttackState.NormalAttack;
            }
            else
            {
                attackTarget = null;
                state = AttackState.ControlWalk;
            }
        }
        if (state == AttackState.NormalAttack)
        {
            if (attackTarget == null)
            {
                return;
            }
            else
            {

                float distance = Vector3.Distance(transform.position, attackTarget.position);
                if (distance <= min_distance)
                {
                    attackState = InAttack.Attack;
                    transform.LookAt(attackTarget);
                    animation1.CrossFade(currentAnimName);
                    timer += Time.deltaTime;
                    if (timer >= time_normalAttack)
                    {
                        if (ShowEffect == false)
                        {
                            ShowEffect = true;
                            attackTarget.GetComponent<WolfBabyManager>().TakeDamage(GetAttack());
                        }
                        currentAnimName = normalAttackIdleName;
                    }
                    if (timer > (1f / attackRate))
                    {
                        timer = 0;
                        ShowEffect = false;
                        currentAnimName = normalAttackName;
                    }
                }
                else
                {
                    attackState = InAttack.Move;
                    control.SimpleMove(attackTarget.position);
                }
            }
        }
    }

    private int GetAttack()
    {
        attack = PlayerStatus.Instance.Attack + PlayerStatus.Instance.Attack_Plus;
        return attack;
    }

    public void UseSkill(SkillInfo info)
    {
        if (PlayerStatus.Instance.playerType == ApplicationType.Magician)
        {
            if (info.applicationType == ApplicationType.Swordman)
            {
                return;
            }
        }
        if (PlayerStatus.Instance.playerType == ApplicationType.Swordman)
        {
            if (info.applicationType == ApplicationType.Magician)
            {
                return;
            }
        }

        switch (info.skillType)
        {
            case SkillType.Passive:
                StartCoroutine(OnPassiveSkillUse(info));
                break;
            case SkillType.Buff:
                StartCoroutine(OnBuffSkillUse(info));
                break;
            case SkillType.SingleTarget:
                OnTargetSkillUse(info);
                break;
            case SkillType.MultiTarget:
                OnTargetSkillUse(info);
                break;
        }
    }

    IEnumerator OnPassiveSkillUse(SkillInfo info)
    {
        state = AttackState.SkillAttack;
        animation1.CrossFade(info.aniName);
        yield return new WaitForSeconds(info.aniTime);
        state = AttackState.ControlWalk;
        switch (info.applyProperty)
        {
            case ApplyProperty.HP:
                PlayerStatus.Instance.AddHp(info.applyValue);
                break;
            case ApplyProperty.MP:
                PlayerStatus.Instance.AddMp(info.applyValue);
                break;
        }
        GameObject effect = null;
        effectDir.TryGetValue(info.effectName, out effect);
        GameObject.Instantiate(effect, transform.position + Vector3.up, Quaternion.identity);

    }

    IEnumerator OnBuffSkillUse(SkillInfo info)
    {
        float addProperty = 0;
        state = AttackState.SkillAttack;
        animation1.CrossFade(info.aniName);
        yield return new WaitForSeconds(info.aniTime);
        state = AttackState.ControlWalk;
        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                addProperty = GetAttack() * ((info.applyValue / 100) - 1);
                PlayerStatus.Instance.GetAttack((int)addProperty);
                break;
            case ApplyProperty.AttackSpeed:
                addProperty = attackRate * ((info.applyValue / 100) - 1);
                attackRate = addProperty + attackRate;
                break;
        }
        GameObject effect = null;
        effectDir.TryGetValue(info.effectName, out effect);
        GameObject.Instantiate(effect, transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(info.applyTime);
        switch (info.applyProperty)
        {
            case ApplyProperty.Attack:
                PlayerStatus.Instance.ReduceAttack((int)addProperty);
                break;
            case ApplyProperty.AttackSpeed:
                attackRate = attackRate - addProperty;
                break;
        }
    }

    void OnTargetSkillUse(SkillInfo info)
    {
        animation1.CrossFade(normalAttackIdleName);
        CursorManager.Instance.SetLock();
        state = AttackState.SkillAttack;
        isLock = true;
        this.info = info;
    }

    private void SkillAttack()
    {
        if (Input.GetMouseButtonDown(0) && isLock)
        {
            isLock = false;
            switch (info.skillType)
            {
                case SkillType.SingleTarget:
                    StartCoroutine(OnSingleTarget());
                    break;
                case SkillType.MultiTarget:
                    StartCoroutine(OnMultiTarget());
                    break;
            }
        }
    }

    IEnumerator OnSingleTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.tag == Tags.Enemy)
            {
                animation1.CrossFade(info.aniName);
                yield return new WaitForSeconds(info.aniTime);
                state = AttackState.ControlWalk;
                GameObject effect = null;
                effectDir.TryGetValue(info.effectName, out effect);
                GameObject.Instantiate(effect, hitInfo.transform.position + Vector3.up, Quaternion.identity);
                CursorManager.Instance.SetNormal();
                hitInfo.transform.GetComponent<WolfBabyManager>().TakeDamage(GetAttack() * (info.applyValue / 100));
            }
            else
            {
                animation1.CrossFade(normalAttackIdleName);
                CursorManager.Instance.SetNormal();
            }
        }
        else
        {
            animation1.CrossFade(normalAttackIdleName);
            CursorManager.Instance.SetNormal();
        }
    }

    IEnumerator OnMultiTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isCollider = Physics.Raycast(ray, out hitInfo,int.MaxValue,1<<LayerMask.NameToLayer("Ground"));

        if (isCollider)
        {
            animation1.CrossFade(info.aniName);
            yield return new WaitForSeconds(info.aniTime);
            state = AttackState.ControlWalk;

            GameObject effect = null;
            effectDir.TryGetValue(info.effectName, out effect);
            GameObject go = Instantiate(effect, hitInfo.point + Vector3.up, Quaternion.identity);
            go.GetComponent<MagicSphereEffect>().attack = GetAttack() * (info.applyValue / 100);
            CursorManager.Instance.SetNormal();
        }
        else
        {
            animation1.CrossFade(normalAttackIdleName);
            CursorManager.Instance.SetNormal();
        }

    }
}
