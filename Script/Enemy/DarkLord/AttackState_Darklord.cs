using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Darklord : EnemyAttackAbleState
{
    // 시간
    private float time;

    // 공격 진입 시간
    private float ADelaytime = 0;

    // 공격 컨트롤러
    private AttackController attackController;

    // 리짓바디
    private Rigidbody rb;

    // 랜덤 공격 타입
    private int ranAttackInt;

    // 저번 공격 타입 기억
    private int beforeAtInt = -1;

    // 지면 충돌 확인
    private bool onGround = true;

    // 발사체
    [SerializeField] private GameObject bulletPrefeb;

    // 큰 발사체
    [SerializeField] private GameObject bigBulletPrefeb;

    // 긴 칼
    [SerializeField] private GameObject longSword;

    // 발사 위치 배열
    [SerializeField] private Transform[] shootPos;

    // 그라운드 히트 이펙트
    [SerializeField] private ParticleSystem groundHit;
    // 순간이동 연기 이펙트
    [SerializeField] private ParticleSystem[] teleportSmokeEffects;

    public override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
        attackController = GetComponent<AttackController>();
    }

    // 공격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 공격을 위해 이동 중지
        navMeshAgent.isStopped = true;

        while (true)
        {
            // 랜덤 공격 패턴 추첨
            ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length);

            if (beforeAtInt != ranAttackInt)
            {
                beforeAtInt = ranAttackInt;
                break;
            }

        }

        // 공격 타입별 시간 입력
        if (ranAttackInt == 0) { ADelaytime = 1.5f; } // 한손 공격
        else if (ranAttackInt == 1) { ADelaytime = 3f; } // 콤보 공격
        else if (ranAttackInt == 2) { ADelaytime = 2.6f; } // 점프 공격
        else if (ranAttackInt == 3) { ADelaytime = 4f; } // 텔레포트 공격
        else if (ranAttackInt == 4) { ADelaytime = 6f; } // 1번 패턴 공격
        else if (ranAttackInt == 5) { ADelaytime = 4.5f; } // 2번 패턴 공격
        else { ADelaytime = 6f; } // 긴 칼 공격


        // 공격 애니메이션 재생
        animator.SetInteger("State", state);

        // 공격 컨트롤러에서 랜덤 패턴 실행
        attackController.TransactionToState(ranAttackInt);
    }

    // 공격 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        LookAtTarget(true);

        if (ADelaytime < time)
        {
            // 공격 대상이 공격 가능 거리보다 멀어졌다면
            if (controller.GetPlayerDistance() > attackDistance)
            {
                // 추격
                controller.TransactionToState(2);
                return;
            }
            else
            {
                // 공격
                controller.TransactionToState(3);
            }


        }

        animator.SetFloat("ATTime", time);
        animator.SetBool("OnGround", onGround);
    }

    

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
        ADelaytime = 0;
        ranAttackInt = 0;
        onGround = false;
    }

    // 각도 발포 이벤트,
    public void Shoot(int num)
    {
        Quaternion rota = shootPos[0].rotation;
        Vector3 rotate = rota.eulerAngles;

        // 서로 마주보는 방면 기준 세로
        if (num == 0)
        {
            Instantiate(bulletPrefeb, shootPos[0].position, shootPos[0].rotation);
        }
        else if (num == 1) // 윗 방면 기준 시계방향으로 45도
        {
            Instantiate(bulletPrefeb, shootPos[0].position, Quaternion.Euler(rotate.x, rotate.y, 45));
        }
        else if (num == 2) // 윗 방면 기준 반시계방향으로 45도
        {
            Instantiate(bulletPrefeb, shootPos[0].position, Quaternion.Euler(rotate.x, rotate.y, -45));
        }
        else // 윗 방면 가로 
        {
            Instantiate(bulletPrefeb, shootPos[0].position, Quaternion.Euler(rotate.x, rotate.y, 90));
        }
    }

    public void BigShoot()
    {
        Quaternion rota = shootPos[0].rotation;
        Vector3 rotate = rota.eulerAngles;

        GetComponent<FSMController_DarkLord>().BigMagicSound();
        Instantiate(bigBulletPrefeb, shootPos[0].position, Quaternion.Euler(rotate.x, rotate.y, 90));
    }

    // 멀티 발포 이벤트
    public void MultiShoot()
    {
        GetComponent<FSMController_DarkLord>().JumpAttackMagicSound();
        for (int i = 0; i < shootPos.Length; i++)
        {
            Instantiate(bulletPrefeb, shootPos[i].position, shootPos[i].rotation);
        }
    }

    public void GroundHitEffectEvent()
    {
        groundHit.Play();
    }

    public void TeleportSmokeEvent(int num)
    {
        if (num == 0)
        {
            // 높은거
            teleportSmokeEffects[0].Play();
        }
        else
        {
            // 낮은거
            teleportSmokeEffects[1].Play();
        }
        
    }

    public void TeleportStart()
    {
        onGround = false;

        // 순간이동전 네브메쉬, 루트모션 해제
        navMeshAgent.enabled = false;
        animator.applyRootMotion = false;
        
        float rangeX = Random.Range(-2f, 2f);
        float rangeZ = Random.Range(-2f, 2f);

        Vector3 pos = new Vector3(
            controller.Player.transform.position.x + rangeX,
            controller.Player.transform.position.y + 3,
            controller.Player.transform.position.z + rangeZ
            );

        GetComponent<FSMController_DarkLord>().TeleportSound();
        gameObject.transform.position = pos;

        TeleportSmokeEvent(1);
    }

    public void TeleportEnd()
    {
        rb.velocity = Vector3.down * 80;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enviroment" && onGround == false && navMeshAgent.enabled == false)
        {
            GetComponent<FSMController_DarkLord>().GroundHitSound();
            GroundHitEffectEvent();
            onGround = true;
            navMeshAgent.enabled = true;
            animator.applyRootMotion = true;
        }
    }

    public void LongSwordGenEventStart()
    {
        GetComponent<FSMController_DarkLord>().BigSwordGenSound();
        longSword.SetActive(true);
        
    }

    public void LongSwordGenEventEnd()
    {
        longSword.SetActive(false);
    }
}
