using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Demon02 : EnemyAttackAbleState
{
    // 악마 1
    private GameObject demon01;

    // 대기 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 대기 애니메이션 재생
        animator.SetInteger("State", state);

        demon01 = GetComponent<FSMController_Demon02>().Demon01;
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        // 2페이즈라면
        if (demon01.GetComponent<Health_Demon01>().Phase == 1)
        {
            // 전장 합류
            controller.TransactionToState(1);
            return;
        }
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
    }
}
