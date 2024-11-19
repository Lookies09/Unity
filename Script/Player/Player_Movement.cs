using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // 이동속도
    [SerializeField] private float moveSpeed;

    // 캐릭터 컨트롤러
    private CharacterController cc;

    // 플레이어 상태 참조
    private Player_Health playerState;

    //애니메이터
    private Animator animator;

    //이동 방향
    private Vector3 direction;

    // 멈춤 확인
    private bool isMoving = false;

    // 구르는중 확인
    private bool isDodge;

    // 움직임 시간
    private float moveTime;

    public bool IsDodge { get => isDodge; set => isDodge = value; }

    private void Awake()
    {
        cc = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerState = GetComponent<Player_Health>();
    }


    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        direction = new Vector3(h, 0, v);

        // 카메라 y축의 y회전각도를 기준으로 캐릭터의 시선 방향벡터를 설정
        direction = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * direction;
        direction.Normalize(); // 방향벡터 정규화함


        // 방향 * 속도
        direction = direction.normalized * moveSpeed;

        // 맞았거나 죽었으면 리턴
        if (GetComponent<Player_Health>().IsHit || GetComponent<Player_Health>().IsDead)
        {
            return;
        }

        // 이동하고 있다면
        if (direction.magnitude > 0.1f && GetComponent<Player_Attack>().IsAttack == false)
        {
            moveTime += Time.deltaTime;

            isMoving = true;            

            // 부드럽게 회전
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 720 * Time.deltaTime);
            
            // 캐릭터 컨트롤러 이동
            cc.Move(direction * Time.deltaTime);

            // 애니메이터 연결
            animator.SetFloat("Movement", direction.magnitude);            
        }
        // 멈추었다면
        else 
        {
            // 멈춤 애니메이션 플레이
            if (isMoving)
            {
                animator.SetTrigger("StopMove");
                isMoving = false;
            }

            moveTime = 0;

            // 애니메이터 연결
            animator.SetFloat("Movement", 0f);
        }

        // 대쉬 설정
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //  현재 스테미너가 사용 스태미너 보다 작다면 리턴
            if (playerState.Stamina < 15) { return; }

            playerState.StaminaUse(15);

            // 공격중이라면 리턴
            if (GetComponent<Player_Attack>().IsAttack) { return; }                       

            IsDodge = true;
        }


        animator.SetBool("Dash", IsDodge);
        animator.SetFloat("MoveTime", moveTime);

        
    }

    // 대쉬 끝내는 이벤트
    public void ExitSlide()
    {
        IsDodge = false;
    }
}
