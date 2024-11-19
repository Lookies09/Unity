using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpState_NormalSkeleton : EnemyAttackAbleState
{
    // 기상 시간
    private float standupTime;

    // 기상 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 애니메이션 재생
        animator.SetInteger("State", (int)state);
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        standupTime += Time.deltaTime;

        // 3초가 안지났으면
        if (standupTime < 3f)
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

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        standupTime = 0;
    }
}
