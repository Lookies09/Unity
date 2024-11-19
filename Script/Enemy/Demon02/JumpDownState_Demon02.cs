using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpDownState_Demon02 : EnemyAttackAbleState
{
    [SerializeField] private AudioSource effectAudio;

    private float jumpTime;

    // 리짓바디
    private Rigidbody rb;

    [SerializeField] private float force; // 초기 힘
    [SerializeField] private float angle; // 각도

    // 땅 접촉 확인
    private bool onGround;

    // 배틀 크라이 이펙트
    [SerializeField] private ParticleSystem battleCryEffect;

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

        // 점프
        Jump();
    }

    // 낙하 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        jumpTime += Time.deltaTime;

        // 중력 조절
        Vector3 customGravity = 1.5f * Physics.gravity;
        rb.AddForce(customGravity, ForceMode.Acceleration);

        // 점프시간이 5초 지났고 땅이라면
        if (jumpTime > 5 && onGround)
        {
            Destroy(rb);

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
                controller.TransactionToState(4);
            }
        }

        animator.SetBool("OnGround", onGround);
        animator.SetFloat("JumpTime", jumpTime);
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        bossUIManager.SetBossInfo(gameObject, "악마 2", 2);
    }

    public void Jump()
    {
        // 각도를 라디안으로 변환
        float radianAngle = angle * Mathf.PI / 180f;

        // 초기 속도 계산
        Vector3 initialVelocity = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0f) * force;

        // 초기 속도 적용
        rb.velocity = initialVelocity;        
    }

    // 지면충돌 확인
    private void OnCollisionEnter(Collision collision)
    {
        // 지면 충돌시
        if (collision.gameObject.tag == "Enviroment")
        {
            effectAudio.Play();

            onGround = true;
            navMeshAgent.enabled = true;
            Debug.Log("지면 출돌");
            jumpTime = 0;

            if (navMeshAgent.enabled == true)
            {
                animator.applyRootMotion = true;
            }
        }
    }

    public void StartBattleCryEvent()
    {
        battleCryEffect.Play();
    }
}
