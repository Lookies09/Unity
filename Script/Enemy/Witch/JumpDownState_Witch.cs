using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class JumpDownState_Witch : EnemyAttackAbleState
{
    private float jumpTime;

    public float jumpForce = 10f; // 기본 힘
    public float xAngle = 45f; // x축 각도 (왼쪽/오른쪽 방향)
    public float yAngle = 45f; // y축 각도 (위쪽 방향)

    // 리짓바디
    private Rigidbody rb;

    private bool onGround;

    // 시작 연기 파티클
    [SerializeField] private ParticleSystem startSmoke;

    // 바닥에 깔리는 마법진
    [SerializeField] private ParticleSystem magicRing;

    [SerializeField] private BossUIManager bossUIManager;

    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    // 낙하 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 애니메이션 재생
        animator.SetInteger("State", state);
        navMeshAgent.enabled = false;
        animator.applyRootMotion = false;
        GetComponent<ObjectHealth>().IsHit = true;
        // 웃는 목소리
        GetComponent<FSMController_Witch>().LaughVoice();
        Jump();
    }

    // 낙하 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        jumpTime += Time.deltaTime;

        // 점프시간이 3초 지났고 땅이라면
        if (jumpTime > 3 && onGround)
        {
            // 플레이거 추격 가능 거리안에 들어왔다면
            if (controller.GetPlayerDistance() <= detectDistance)
            {
                // 추적 상태
                controller.TransactionToState(2);
                return;
            }

            // 공격가능 거리안에 들어왔다면
            if (controller.GetPlayerDistance() <= attackDistance)
            {
                // 공격 상태
                controller.TransactionToState(3);
            }
        }

        animator.SetBool("OnGround", onGround);
        animator.SetFloat("JumpTime", jumpTime);
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        GetComponent<ObjectHealth>().IsHit = false;

        bossUIManager.SetBossInfo(gameObject, "마녀");
    }

    void Jump()
    {
        // 각도를 라디안으로 변환
        float xAngleRad = xAngle * Mathf.Deg2Rad;
        float yAngleRad = yAngle * Mathf.Deg2Rad;

        // x축과 y축 속도 계산
        float xVelocity = jumpForce * Mathf.Cos(yAngleRad) * Mathf.Cos(xAngleRad);
        float yVelocity = jumpForce * Mathf.Sin(yAngleRad);
        float zVelocity = jumpForce * Mathf.Cos(yAngleRad) * Mathf.Sin(xAngleRad);

        // 벡터 합성
        Vector3 jumpDirection = new Vector3(xVelocity, yVelocity, zVelocity);

        // 기존 속도를 초기화하고 점프 속도를 설정
        rb.velocity = jumpDirection;
    }


    // 지면충돌 확인
    private void OnCollisionEnter(Collision collision)
    {
        // 지면 충돌시
        if (collision.gameObject.tag == "Enviroment")
        {
            // 착지 소리 재생
            GetComponent<FSMController_Witch>().MovementSound(0);
            navMeshAgent.enabled = true;
            Debug.Log("지면 출돌");
            jumpTime = 0;
            onGround = true;            

            if (navMeshAgent.enabled == true)
            {
                onGround = true;
                Destroy(rb);
                animator.applyRootMotion = true;
            }
        }
    }

    public void StartSmokeEvent()
    {
        GetComponent<FSMController_Witch>().SKillAttackVoice();
        startSmoke.Play();
        magicRing.Play();
    }
}
