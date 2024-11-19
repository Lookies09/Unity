using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalkState_Demon : EnemyAttackAbleState
{
    // 악마 2
    private GameObject demon02;

    // 상태 전환 가능 여부
    private bool canAttack = false;

    // 옆걸음 시간
    private float time;

    // 옆걸음 방향 [좌:0/우:1]
    private int walkDirc;

    public bool CanAttack { get => canAttack; set => canAttack = value; }

    public void Start()
    {
        // 악마 2 참조
        demon02 = GetComponent<FSMController_Demon01>().Demon02;
    }

    // 옆걸음 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 옆걸음 애니메이션 재생
        animator.SetInteger("State", state);

        walkDirc = Random.Range(0, 2);
        animator.SetInteger("WalkDirc", walkDirc);

        // 정지
        navMeshAgent.isStopped = true;        

        // 나는 공격 중지
        canAttack = false;

        if (GetComponent<ObjectHealth>().Phase < 2)
        {
            // 다른 악마는 공격 시작
            demon02.GetComponent<SideWalkState_Demon02>().CanAttack = true;
        }
        
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        animator.SetFloat("SideWalkTime", time);

        // 플레이어 바라보기
        LookAtTarget(true);


        // 내가 공격 가능 상태라면
        if (CanAttack || GetComponent<ObjectHealth>().Phase == 2)
        {
            // 추격
            controller.TransactionToState(2);
        }
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
    }
}
