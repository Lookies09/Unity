using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState_Skeleton_Ranger : EnemyAttackAbleState
{
    // 피격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 피격 애니메이션 재생
        animator.SetInteger("State", (int)state);
    }

    // 피격 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        // 맞고있으면 리턴
        if (GetComponent<ObjectHealth>().IsHit)
        {
            return;
        }


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

    // 피격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
    }
}
