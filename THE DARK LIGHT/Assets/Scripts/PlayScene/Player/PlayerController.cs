using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PlayerState
{
    Idel,
    Move,
}
public class PlayerController : MonoBehaviour {
    public GameObject MousePrefab;
    private EventSystem eventSystem;
    private GraphicRaycaster graphicRaycaster;
    private GameObject MouseEffectGO;
    private bool isMoving = false;
    private Vector3 TargetPosition;
    private PlayerState state = PlayerState.Idel;
    private PlayerAttack attack;
    // Use this for initialization
    void Start() {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        graphicRaycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        attack = GetComponent<PlayerAttack>();
        InstanceMouseEffect();
        TargetPosition = this.transform.position;

    }

    // Update is called once per frame
    void Update() {
        DetectionClickOnGround();
        PlayerMoving();


    }

    private void LateUpdate()
    {
        ChangeAnimation();
    }

    void InstanceMouseEffect()
    {
        MouseEffectGO = Instantiate(MousePrefab);
        MouseEffectGO.SetActive(false);
    }

    void HideMouseEffect()
    {
        MouseEffectGO.SetActive(false);
    }
    bool CheckGuiRaycastObjects()
    {
        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;

        List<RaycastResult> list = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, list);
        return list.Count > 0;
    }
    void DetectionClickOnGround()
    {
        if (CheckGuiRaycastObjects()) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.tag == Tags.Ground)
                {
                    MouseEffectGO.transform.position = hitInfo.point + new Vector3(0, 0.2f, 0);
                    MouseEffectGO.SetActive(true);
                    Invoke("HideMouseEffect", 0.32f);
                    isMoving = true;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
    }

    void PlayerMoving()
    {
        if (isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.tag == Tags.Ground)
                {
                    TargetPosition = new Vector3(hitInfo.point.x, this.transform.position.y, hitInfo.point.z);
                    this.transform.LookAt(TargetPosition);
                }
            }
        }
        if (attack.state == AttackState.ControlWalk)
        {
            if (Mathf.Abs(Vector3.Distance(this.transform.position, TargetPosition)) >= 0.5f)
            {
                state = PlayerState.Move;
                this.transform.GetComponent<CharacterController>().SimpleMove(transform.forward * 4);
            }
            else
            {
                state = PlayerState.Idel;
            }
        }
    }


    void ChangeAnimation()
    {
        if (attack.state == AttackState.ControlWalk)
        {
            if (state == PlayerState.Idel)
            {
                PlayAnimation("Idle");
            }
            else if (state == PlayerState.Move)
            {
                PlayAnimation("Walk", 3);
            }
        }else if(attack.state == AttackState.NormalAttack)
        {
            if(attack.attackState == InAttack.Move)
            {
                PlayAnimation("Walk", 3);
            }
        }


    }
   
    void PlayAnimation(string animationName,int animationPlaySpeed = 1)
    {
        this.transform.GetComponent<Animation>().CrossFade(animationName);
        this.transform.GetComponent<Animation>()[animationName].speed = animationPlaySpeed;
    }

    public void SimpleMove(Vector3 position)
    {
        transform.LookAt(position);
        this.transform.GetComponent<CharacterController>().SimpleMove(transform.forward * 4);
    }

}
