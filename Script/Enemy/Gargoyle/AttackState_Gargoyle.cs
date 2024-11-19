using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Gargoyle : EnemyAttackAbleState
{
    // 시간
    private float time;

    // 공격 진입 시간
    private float ADelaytime = 0;

    // 공격 컨트롤러
    private AttackController attackController;

    // 랜덤 공격 타입
    private int ranAttackInt;

    // 저번 공격 타입 기억
    private int beforeAtInt = -1;

    // 탄환 발사 위치
    [SerializeField] private Transform shootPos;

    // 탄환 프리펩
    [SerializeField] private GameObject[] bulletPrefabs;

    public override void Awake()
    {
        base.Awake();

        attackController = GetComponent<AttackController>();
    }

    // 공격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 공격을 위해 이동 중지
        navMeshAgent.isStopped = true;

        
        while (true)
        {
            // 지난 공격과 안겹치는 랜덤 공격 패턴 추첨
            ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length+1);

            if (beforeAtInt != ranAttackInt)
            {
                beforeAtInt = ranAttackInt;
                break;
            }

        }

        // 공격 타입별 시간 입력
        if (ranAttackInt == 0) { ADelaytime = 1.5f; } // 기본 공격
        else if (ranAttackInt == 1) { ADelaytime = 2.7f; } // 회전 콤보 공격
        else if (ranAttackInt == 2) { ADelaytime = 1f; } // 한번 공격
        else { controller.TransactionToState(4); return; }


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

        animator.SetFloat("ATime", time);
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
        ADelaytime = 0;
        ranAttackInt = 0;
    }

    // 발포 이벤트,
    public void Shoot(int type)
    {
        if (type == 0)
        {
            Instantiate(bulletPrefabs[0], shootPos.position, shootPos.rotation);

            GetComponent<FSMController_Gargoyle>().MagicSound(0);
        }
        else
        {
            Instantiate(bulletPrefabs[1], shootPos.position, shootPos.rotation);
            GetComponent<FSMController_Gargoyle>().MagicSound(1);
        }

    }
}
