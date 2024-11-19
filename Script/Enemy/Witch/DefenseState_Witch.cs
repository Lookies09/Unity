using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseState_Witch : EnemyAttackAbleState
{
    // 방어 지속 시간
    [SerializeField] private float defenseTime;

    // 2페이즈 연기
    [SerializeField] private ParticleSystem phase2Smoke;

    // 파장 이펙트
    [SerializeField] private ParticleSystem areaEffect;

    // 머리카락 게임오브젝트
    [SerializeField] private GameObject hair;

    // 메트리얼 게임오브젝트
    [SerializeField] private Material whiteMatrial;    

    // 시간
    private float time;

    // 방어 이펙트
    [SerializeField] private ParticleSystem defenseEffect;

    // 방어 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 네비게이션 에이전트 이동 정지
        navMeshAgent.isStopped = true;

        // 방어 애니메이션 재생
        animator.SetInteger("State", state);

        // 히트 상태로 안넘어 가도록
        GetComponent<ObjectHealth>().IsHit = true;

        defenseEffect.Play();
    }

    // 방어 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        animator.SetFloat("DefenseTime", time);

        // 방어 시간이 안지났으면 리턴
        if (defenseTime > time) { return; }

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

    // 방어 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;       

        // 히트 상태로 안넘어 가도록
        GetComponent<ObjectHealth>().IsHit = false;        
    }

    public void Phase2Event()
    {
        // 방어 목소리
        GetComponent<FSMController_Witch>().OnShieldVoice();
        phase2Smoke.Play();
        areaEffect.Play();
        hair.GetComponent<SkinnedMeshRenderer>().material = whiteMatrial;
    }
}
