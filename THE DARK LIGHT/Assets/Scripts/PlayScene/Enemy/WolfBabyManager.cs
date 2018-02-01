using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum WolfState
{
    Idle,
    Attack,
    Walk,
    Death
}

public class WolfBabyManager : MonoBehaviour {


    public WolfState wolfState = WolfState.Idle;
    public float redTime = 1;
    private string deathString = "WolfBaby-Death";
    private string idleString = "WolfBaby-Idle";
    private string walkString = "WolfBaby-Walk";
    private string attack1String = "WolfBaby-Attack1";
    private string attack2String = "WolfBaby-Attack2";


    public int hp = 100;
    public int attack = 5;
    public float maxAttackDistance = 0.3f;
    public float maxFindDistance = 5;
    private float miss_rate = 0.2f;
    private string currentAniName;
    private string currentAttack;
    private float attackTime = 2f;
    private float attackTimer = 0f;
    private Animation animation1;
    private CharacterController cc;

    private float time = 2;
    private float timer = 0;
    private Renderer render;
    private Color normal;
    private bool isFindPlayer = false;
    private bool isCanAttack = false;


    public GameObject miss;


    private void OnMouseEnter()
    {
        if (PlayerAttack.Instance.isLock == false)
        {
            CursorManager.Instance.SetAttack();
        }
    }
    private void OnMouseExit()
    {
        if (PlayerAttack.Instance.isLock == false)
        {
            CursorManager.Instance.SetNormal();
        }
    }
    // Use this for initialization
    void Start () {
        isFindPlayer = false;
        isCanAttack = false;
        animation1 = GetComponent<Animation>();
        cc = GetComponent<CharacterController>();
        render = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        normal = render.material.color;
        currentAniName = idleString;
	}
	
	// Update is called once per frame
	void Update () {
        ControlAnimPlay();
        CheckDistance();

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    wolfState = WolfState.Attack;
        //}
    }
    private void CheckDistance()
    {
        if(Mathf.Abs(Vector3.Distance(this.transform.position,PlayerStatus.Instance.gameObject.transform.position)) < maxFindDistance)
        {
            if (isCanAttack == false)
            {
                isFindPlayer = true;
                wolfState = WolfState.Walk;
                currentAniName = walkString;
                animation1.CrossFade(currentAniName);
                this.transform.LookAt(PlayerStatus.Instance.transform.position);
                cc.SimpleMove(transform.forward * 1);
            }
            if (Mathf.Abs(Vector3.Distance(this.transform.position, PlayerStatus.Instance.gameObject.transform.position)) < maxAttackDistance)
            {
                isCanAttack = true;
                wolfState = WolfState.Attack;
            }
            else
            {
                isCanAttack = false;
            }
        }
        else
        {
            isFindPlayer = false;
        }
    }

    private void Attack()
    {
        float value = Random.Range(0f, 1f);
        if (value > 0.5f)
        {
            currentAttack = attack1String;
            AttackPlayer();
        }
        else
        {
            currentAttack = attack2String;
            AttackPlayer();
        }
    }

    public void AttackPlayer()
    {
        PlayerStatus.Instance.ReduceHp(attack);
        animation1.CrossFade(currentAttack);
    }

    private void ControlAnimPlay()
    {
        if (wolfState == WolfState.Attack)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer>= attackTime)
            {
                attackTimer = 0;
                Attack();
            }
        }
        else if (wolfState == WolfState.Death)
        {
            animation1.CrossFade(deathString);
        }
        else
        {
            if (isFindPlayer == false)
            {
                if (currentAniName == walkString)
                {
                    cc.SimpleMove(transform.forward * 1);
                }
                animation1.CrossFade(currentAniName);
                timer += Time.deltaTime;
                if (timer > time)
                {
                    timer = 0;
                    RandomState();
                }
            }
        }
    }
    private void RandomState()
    {
        int index = Random.Range(0, 2);
        if(index == 0)
        {
            if (currentAniName != walkString)
            {
                transform.Rotate(transform.up * Random.Range(0, 360));
            }
            currentAniName = walkString;
        }
        else if(index == 1)
        {
            currentAniName = idleString;
        }
    }

    public void TakeDamage(int damage,float time = 0.1f)
    {
        if (wolfState == WolfState.Death) return;
        float value = Random.Range(0f, 1f);
        if (value < miss_rate)
        {
            StartCoroutine(AttackMiss());
        }
        else
        {
            hp -= damage;
            StartCoroutine("ShowBodyRed", time);
            if (hp <= 0)
            {
                PlayerStatus.Instance.AddExp(20);
                UIQuestSystem.Instance.KillCount++;
                wolfState = WolfState.Death;
                Destroy(this.gameObject, 0.5f);
            }
        } 
    }

   IEnumerator AttackMiss()
    {
        miss.SetActive(true);
        miss.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.1f);
        miss.SetActive(false);
    }

    IEnumerator ShowBodyRed(float time)
    {
        render.material.color = Color.red;
        yield return new WaitForSeconds(time);
        render.material.color = normal;
    }

}
