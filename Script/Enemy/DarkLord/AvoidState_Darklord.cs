using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvoidState_Darklord : EnemyAttackAbleState
{
    // 시간
    private float time;

    // 회피 방향
    private int avoidDirec = -1;

    // 회피 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        avoidDirec = Random.Range(0, 2);


        animator.SetInteger("AvoidDirec", avoidDirec);
        // 회피 애니메이션 재생
        animator.SetInteger("State", state);
    }

    // 대기 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        // 0.5초 안지났으면 리턴
        if (time < 0.3f) { return; }

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

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
    }
}
