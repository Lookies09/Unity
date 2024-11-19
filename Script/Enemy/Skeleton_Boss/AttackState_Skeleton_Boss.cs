using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Skeleton_Boss : EnemyAttackAbleState
{
    // 시간
    private float time;

    // 랜덤 공격 타입
    private int ranAttackInt;

    // 저번 공격 타입 기억
    private int beforeAtInt = -1;

    // 공격 진입 시간
    private float ADelaytime = 0;

    // 공격 컨트롤러
    private AttackController attackController;

    // 투사체 발사 위치들
    [SerializeField ] private Transform[] shootPoses;

    // 투사체
    [SerializeField] private GameObject bulletPrefab;

    // 투사체 세로
    [SerializeField] private GameObject verticalBullet;

    // 공격 오디오
    [SerializeField] private AudioSource battleAudio;

    [SerializeField] private AudioClip[] attackClips;

    // 이펙트 오디오
    [SerializeField] private AudioSource effectAudio;


    public override void Awake()
    {
        base.Awake();

        attackController = GetComponent<AttackController>();
    }

    // 공격 상태 시작(진입) 처리(상태 초기화)
    public override void EnterState(int state)
    {
        // 공격 애니메이션 재생
        animator.SetInteger("State", state);

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
        if (ranAttackInt == 0) { ADelaytime = 2.5f; } // 콤보 1 공격
        else if (ranAttackInt == 1) { ADelaytime = 2f; } // 기본 공격

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
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
        ADelaytime = 0;
        ranAttackInt = 0;
    }

    // 발포 이벤트,
    public void Shoot(int type)
    {
        if (type == 0)
        {
            for (int i = 0; i < shootPoses.Length; i++)
            {

                Instantiate(bulletPrefab, shootPoses[i].position, shootPoses[i].rotation);
                effectAudio.Play();
            }
        }
        else
        {
            for (int i = 0; i < shootPoses.Length; i++)
            {
                Instantiate(verticalBullet, shootPoses[i].position, shootPoses[i].rotation);
                effectAudio.Play();
            }
        }
        
    }

    public void AttackAudioPlay()
    {
        int num = Random.Range(0, attackClips.Length);

        battleAudio.clip = attackClips[num];

        battleAudio.Play();
    }
}
