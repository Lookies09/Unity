using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_NormalSkeleton : EnemyAttackAbleState
{
    // 공격 시간
    private float aTime;

    // 공격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 공격 애니메이션 재생
        animator.SetInteger("State", state);
    }

    // 공격 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        aTime += Time.deltaTime;

        LookAtTarget(true);

        // 1.5초가 지나면
        if (aTime > 1.5f)
        {
            // 플레이어가 공격 가능거리보다 멀어지면
            if (controller.GetPlayerDistance() > attackDistance)
            {
                // 추격 상태
                controller.TransactionToState(2);
                return;
            }
            else
            {
                // 공격 상태
                controller.TransactionToState(3);
                return;
            }
        }

        
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        aTime = 0;
    }
}
