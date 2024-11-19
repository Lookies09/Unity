using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_NormalSkeleton : EnemyState
{
    // 사망 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 이동 중지
        navMeshAgent.isStopped = true;

        // 사망 애니메이션 재생
        animator.SetBool("Dead", true);
    }

    // 사망 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        // 몬스터가 소멸됨
        Destroy(gameObject,3);
    }

    // 사망 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {

    }
}
