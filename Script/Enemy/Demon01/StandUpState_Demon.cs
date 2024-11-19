using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpState_Demon : EnemyAttackAbleState
{
    // 기상 시간
    private float standupTime;

    // 칼 게임오브젝트 참조
    [SerializeField] private GameObject axeOnBack; // 등 뒤에 도끼
    [SerializeField] private GameObject axeOnBattle; // 전투시 도끼

    // 시작시 연기
    [SerializeField] private ParticleSystem startSmoke;

    // 울음 이펙트
    [SerializeField] private ParticleSystem battleCryEffect;

    // 아우라 이펙트
    [SerializeField] private ParticleSystem auraEffect;

    [SerializeField] private BossUIManager bossUIManager;

    // 기상 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 애니메이션 재생
        animator.SetInteger("State", state);
    }

    // 기상 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        standupTime += Time.deltaTime;
        animator.SetFloat("StandupTime", standupTime);

        // 1초가 안지났으면
        if (standupTime < 13f)
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
            controller.TransactionToState(4);
        }
    }

    // 대기 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        standupTime = 0;
        bossUIManager.SetBossInfo(gameObject, "악마 1");
    }
   

    public void ChangeAxeEvent()
    {
        axeOnBack.SetActive(false);
        axeOnBattle.SetActive(true);
    }

    public void BattleCryEffect()
    {
        startSmoke.Play();

        battleCryEffect.Play();
        auraEffect.Play();
    }
}
