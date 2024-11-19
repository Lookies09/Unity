using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Demon : EnemyAttackAbleState
{
    // 배틀 오디오 (도끼 소리)
    [SerializeField] private AudioSource battleAudio;

    // 이펙트 사운드 오디오 (땅 찍는 소리)
    [SerializeField] private AudioSource effectAudio;

    // 목소리 오디오소스
    [SerializeField] private AudioSource voiceAudio;

    // 공격 목소리 클립
    [SerializeField] private AudioClip[] attackvoices;

    // 그라운드 히트 이펙트
    [SerializeField] private ParticleSystem groundHitEffect;

    // 시간
    private float time;

    // 공격 진입 시간
    private float ADelaytime = 0;

    // 공격 컨트롤러
    private AttackController attackController;

    // 랜덤 공격 타입
    private int ranAttackInt;

    // 저번 공격 타입 기억
    private int beforeAtInt = 0;


    public override void Awake()
    {
        base.Awake();

        attackController = GetComponent<AttackController>();
    }

    // 공격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {       
        // 공격을 위해 이동 중지
        navMeshAgent.isStopped = true;
                
        while (true)
        {
            // 랜덤 공격 패턴 추첨
            ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length);            

            if (beforeAtInt != ranAttackInt)
            {
                beforeAtInt = ranAttackInt;
                break;
            }

        }
                
        // 공격 타입별 시간 입력
        if (ranAttackInt == 0) { ADelaytime = 1.3f; } // 한손 공격
        else if (ranAttackInt == 1) { ADelaytime = 2f; } // 회전 공격
        else if (ranAttackInt == 2) { ADelaytime = 3.5f; } // 점프 공격


        // 공격 애니메이션 재생
        animator.SetInteger("State", state);

        // 공격 컨트롤러에서 랜덤 패턴 실행
        attackController.TransactionToState(ranAttackInt);
    }

    // 공격 상태 기능 동작 처리 (상태 실행)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        LookAtTarget(true);

        if (ADelaytime < time)
        {
            // 공격 대상이 공격 가능 거리보다 멀어졌다면
            if (controller.GetPlayerDistance() > attackDistance)
            {                
                // 1 페이즈 아니면 3 페이즈 일시
                if (GetComponent<ObjectHealth>().Phase == 0 || GetComponent<ObjectHealth>().Phase == 2)
                {
                    // 추격
                    controller.TransactionToState(2);
                    return;
                }
                else
                {
                    // 옆 걷기
                    controller.TransactionToState(3);
                    return;
                }
            }
            else
            {
                // 공격
                controller.TransactionToState(4);
            }


        }
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
        ADelaytime = 0;
        ranAttackInt = 0;
    }

    public void AttackVoiceSound()
    {
        if (voiceAudio.isPlaying)
        {
            voiceAudio.Stop();
        }

        int num = Random.Range(0, attackvoices.Length);

        voiceAudio.clip = attackvoices[num];

        voiceAudio.Play();

    }

    public void AttackSound()
    {
        battleAudio.Play();
    }

    public void EffectSound()
    {
        groundHitEffect.Play();
        effectAudio.Play();
    }
}
