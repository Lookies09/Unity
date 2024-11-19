using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartoonFX;

public class DownState_Gargoyle : EnemyAttackAbleState
{
    private float dropTime;

    // 리짓바디
    private Rigidbody rb;

    // 땅 접촉 확인
    private bool onGround;

    // 땅 먼지 이펙트
    [SerializeField] private ParticleSystem groundHitEffect;

    // 카메라 쉐이크
    [SerializeField] private GameObject cameraShake;

    // 손에 있는 폴암
    [SerializeField] private GameObject polearmOnHand;

    // 몸 아우라
    [SerializeField] private ParticleSystem bodyAura;

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

        // 가고일 떨구기
        DropDown();
    }

    // 낙하 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        dropTime += Time.deltaTime;


        // 점프시간이 5초 지났고 땅이라면
        if (dropTime > 5 && onGround)
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
        animator.SetFloat("DropTime", dropTime);
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        animator.SetBool("OnGround", false);
        bossUIManager.SetBossInfo(gameObject, "가고일");

    }

    public void DropDown()
    {
        rb.velocity = Vector3.down * 50;
    }


    // 지면충돌 확인
    private void OnCollisionEnter(Collision collision)
    {
        // 지면 충돌시
        if (collision.gameObject.tag == "Enviroment" && controller.CurrentState == controller.EnemyStates1[1])
        {
            GetComponent<FSMController_Gargoyle>().DownSound();


            groundHitEffect.Play();
            Instantiate(cameraShake, transform.position, Quaternion.identity);
            bodyAura.Play();
            onGround = true;
            navMeshAgent.enabled = true;
            dropTime = 0;

            if (navMeshAgent.enabled == true)
            {
                animator.applyRootMotion = true;
            }
        }
    }

    public void PolearmGenEvent()
    {
        polearmOnHand.SetActive(true);
    }
}
