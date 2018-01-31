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
    // Use this for initialization
    void Start()
    {
        control = GetComponent<PlayerController>();
        animation1 = GetComponent<Animation>();
        currentAnimName = normalAttackName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

    public int GetAttack()
    {
        attack = PlayerStatus.Instance.Attack + PlayerStatus.Instance.Attack_Plus;
        return attack;
    }
}
