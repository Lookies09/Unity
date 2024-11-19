using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Demon : EnemyAttackAbleState
{
    // 대기 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 대기 애니메이션 재생
        animator.SetInteger("State", state);
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        // 플레이거 추격 가능 거리안에 들어왔다면
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            // 일어남 상태
            controller.TransactionToState(1);
            return;
        }
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
    }
}
