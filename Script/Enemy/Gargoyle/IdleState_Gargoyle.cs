using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Gargoyle : EnemyAttackAbleState
{
    [SerializeField] private GameObject[] demons;

    // 대기 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 대기 애니메이션 재생
        animator.SetInteger("State", state);

        // 네비게이션 에이전트 끄기
        navMeshAgent.enabled = false;

        // 애니메이터 루트모션 끄기
        animator.applyRootMotion = false;

        GetComponent<Collider>().isTrigger = true;
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        if (demons[0] == null && demons[1] == null)
        {
            // 전장 합류
            controller.TransactionToState(1);
            return;
        }        
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {

        GetComponent<Collider>().isTrigger = false;
    }


}
