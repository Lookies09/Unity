using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipState_Darklord : EnemyAttackAbleState
{
    // 측정 시간
    private float time;

    // 검 생성 연기 이펙트
    [SerializeField] private ParticleSystem swordGenSmoke;

    // 검 오브젝트
    [SerializeField] private GameObject sword;

    [SerializeField] private BossUIManager bossUIManager;

    // 착검 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 착검 애니메이션 재생
        animator.SetInteger("State", state);
    }

    // 착검 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        // 4초가 안지났으면 리턴
        if (time < 4f) { return; }

        // 플레이거 추격 가능 거리안에 들어왔다면
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            if (controller.GetPlayerDistance() <= attackDistance)
            {
                // 공격 상태
                controller.TransactionToState(3);
                return;
            }

            // 추격 상태
            controller.TransactionToState(2);
        }
        
    }

    // 착검 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
        bossUIManager.SetBossInfo(gameObject, "어둠 군주");
    }

    public void EquipSmokeEvent()
    {
        swordGenSmoke.Play();
    }

    public void SetSwordEvent()
    {
        sword.SetActive(true);
    }


}
