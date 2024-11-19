using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class JumpDownState_Gargoyle : EnemyAttackAbleState
{
    [SerializeField] private Animator wingAnimtor;

    [SerializeField] private ParticleSystem areaEffect;

    // 땅 먼지 이펙트
    [SerializeField] private ParticleSystem groundHitEffect;

    // 카메라 쉐이크
    [SerializeField] private GameObject cameraShake;

    // 리짓바디
    private Rigidbody rb;

    // 점프 시간
    private float time;

    // 지면 충돌
    private bool onGround = false;

    // 낙하 확인
    private bool onDown = false;

    // ===================================================================

    // 착지 공격 대상 레이어
    [SerializeField] private LayerMask targetLayer;

    // 착지 공격 범위
    [SerializeField] private float attackRadius;


    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    // 점프 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 점프 애니메이션 재생
        animator.SetInteger("State", state);

        // 날개짓 활성화
        wingAnimtor.enabled = true;

        navMeshAgent.enabled = false;
        animator.applyRootMotion = false;                
    }

    // 점프 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        if (time <= 3f && time > 0.5f && !onGround)
        {
            Vector3 pos = controller.Player.transform.position;
            pos.y = transform.position.y;

            if (Vector3.Distance(transform.position, pos) > 3)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, 10 * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(pos - transform.position, Vector3.up);


            }
            else
            {

                transform.rotation = transform.rotation;
            }
        }

        if (time > 3f && !onGround) // 3초 지났다면
        {            
            rb.velocity = Vector3.down * 100;
            onDown = true;
        }

        if (onGround && time > 1.5f)
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


        animator.SetFloat("DropTime", time);
        animator.SetBool("OnDown", onDown);
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
        onGround = false;
        onDown = false;
        animator.SetBool("OnGround", onGround);

        // 날개짓 활성화
        wingAnimtor.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enviroment" && time > 2f)
        {
            // 영역, 히트 이펙트, 카메라쉐이크 재생
            areaEffect.Play();
            groundHitEffect.Play();

            LandingAttack();

            Instantiate(cameraShake, transform.position, Quaternion.identity);
            GetComponent<FSMController_Gargoyle>().DownSound();

            // 날개짓 비활성화
            wingAnimtor.enabled = false;
            onGround = true;
            navMeshAgent.enabled = true;
            animator.SetBool("OnGround", onGround);
            time = 0;

            if (navMeshAgent.enabled == true)
            {
                animator.applyRootMotion = true;
            }
        }
    }

    public void JumpUp()
    {
        GetComponent<FSMController_Gargoyle>().JumpSound();
        onDown = false;
        rb.velocity = Vector3.up * 30;
        
    }

    public void LandingAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            if (hit.tag == "Player")
            {
                Debug.Log("착지 데미지 입음");

                hit.gameObject.GetComponent<ObjectHealth>().Hit(10);
                break;
            }

        }
    }
}
