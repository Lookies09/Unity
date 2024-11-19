using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StandUpState_Skeleton_Boss : EnemyAttackAbleState
{
    // 기상 시간
    private float standupTime;

    // 칼 게임오브젝트 참조
    [SerializeField] private GameObject swordSitState; // 앉아있을때 칼
    [SerializeField] private GameObject swordBattleState; // 전투시 칼

    // 시작시 연기
    [SerializeField] private ParticleSystem startSmoke;

    // 보스 상태 UI 매니저 참조
    [SerializeField] private BossUIManager bossUIManager;



    // 기상 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 애니메이션 재생
        animator.SetInteger("State", (int)state);        
    }

    // 기상 상태 기능 동작 처리 (상태 실행)
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

        // 보스 UI에 정보 입력
        bossUIManager.SetBossInfo(gameObject, "스켈레톤 보스");
    }

    // 팔 ik 전환
    public void StandUpEvent(int num)
    {
        if (num == 0)
        {
            GetComponent<RigBuilder>().layers[1].active = false;
        }
        else
        {            
            GetComponent<RigBuilder>().layers[0].active = false;
        }

        
    }

    public void ChangeSwordEvent()
    {
        // 칼 교체
        swordSitState.SetActive(false);
        swordBattleState.SetActive(true);
    }

    public void ChangeSwordSmokeEvent()
    {
        startSmoke.Play();
    }


}
