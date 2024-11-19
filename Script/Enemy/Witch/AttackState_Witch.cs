using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Witch : EnemyAttackAbleState
{
    [SerializeField] private AudioSource battleAudio;

    // 0번 - 직선공격, 1번 - 추격공격 소리
    [SerializeField] private AudioClip[] normalAttackSounds;

    // 원형 공격 사운드
    [SerializeField] private AudioClip roundAttackSound;

    // 소환 공격 사운드
    [SerializeField] private AudioClip summonAttackSound;

    // 레이저 공격 사운드
    [SerializeField] private AudioClip lazerAttackSound;

    // 시간
    private float time;

    // 레이저 히트 시간
    private float laserHitTime;

    // 랜덤 공격 타입
    private int ranAttackInt;

    // 공격 진입 시간
    private float ADelaytime = 0;

    // 저번 공격 타입 기억
    private int beforeAtInt = 0;

    // 공격 컨트롤러
    private AttackController attackController;

    // 기본 투사체 발사 위치
    [SerializeField] private Transform shootPos;

    // 영역 투사체 발사 위치들
    [SerializeField] private Transform[] shootPoses;

    // 오버랩 끝나는 위치
    [SerializeField] private Transform overlapEndPos;

    // 기본 투사체
    [SerializeField] private GameObject bulletPrefab;

    // 추격 투사체
    [SerializeField] private GameObject traceBullet;

    // 영역 이펙트
    [SerializeField] private ParticleSystem areaEffect;

    // 소환 스켈레톤 프리펩
    [SerializeField] private GameObject summonMonster;

    // 소환 이펙트
    [SerializeField] private ParticleSystem summonEffect;

    // 소환할때 몬스터 밑에 생기는 이펙트
    [SerializeField] private GameObject monsterSummonEffect;

    // 소환 위치들
    [SerializeField] private Transform[] summonPoses;

    // 레이저 프리펩
    [SerializeField] private ParticleSystem laserPrefab;

    // 오버랩 둘래
    [SerializeField] private float maxRayLength;

    // 레이저 공격 시작 확인
    private bool onLaserAT;

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

        // 공격시 히트 애니메이션으로 안넘어가게
        GetComponent<ObjectHealth>().IsHit = true;

        // 1페이즈라면
        if (gameObject.GetComponent<Health_Witch>().Phase == 0)
        {
            while (true)
            {
                // 랜덤 공격 패턴 추첨
                ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length -2 );

                if (beforeAtInt != ranAttackInt)
                {
                    beforeAtInt = ranAttackInt;
                    break;
                }

            }
        }
        else
        {
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
        }
        

        // 공격 타입별 시간 입력
        if (ranAttackInt == 0) { ADelaytime = 1.3f; } // 한손 공격
        else if (ranAttackInt == 1) { ADelaytime = 1.5f; } // 양손 공격
        else if (ranAttackInt == 2) { ADelaytime = 3f; } // 영역 공격
        else if (ranAttackInt == 3) { ADelaytime = 3.5f; } // 소환
        else { ADelaytime = 5f; } // 레이저 공격

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

        if (onLaserAT)
        {
            OverlapCreate();
        }
    }

    // 공격 상태 종료(다른상태로 전이) 동작 처리(상태 정리)
    public override void ExitState()
    {
        time = 0;
        ADelaytime = 0;
        ranAttackInt = 0;
        GetComponent<ObjectHealth>().IsHit = false;
    }

    // 기본 공격
    public void Shoot()
    {
        battleAudio.clip = normalAttackSounds[0];
        battleAudio.Play();
        Instantiate(bulletPrefab, shootPos.position, shootPos.rotation);
    }

    // 추격 공격
    public void TraceShoot()
    {
        battleAudio.clip = normalAttackSounds[1];
        battleAudio.Play();
        Instantiate(traceBullet, shootPos.position, Quaternion.identity);
    }

    // 영역 발사 공격
    public void AreaShoot()
    {
        areaEffect.Play();
        // 1 페이즈면
        if (gameObject.GetComponent<Health_Witch>().Phase == 0)
        {
            // 10곳에서만 발사
            for (int i = 0; i < shootPoses.Length-10; i++)
            {
                Instantiate(bulletPrefab, shootPoses[i].position, shootPoses[i].rotation);
            }
        }
        else // 2페이즈면
        {
            // 전체 발사
            for (int i = 0; i < shootPoses.Length; i++)
            {
                Instantiate(bulletPrefab, shootPoses[i].position, shootPoses[i].rotation);
            }
        }

        battleAudio.clip = roundAttackSound;
        battleAudio.Play();

    }

    // 소환 이벤트
    public void Summon()
    {
        summonEffect.Play();

        battleAudio.clip = summonAttackSound;
        battleAudio.Play();

        // 몇마리 소환? 지금은 6마리
        for (int i = 0; i < 6; i++)
        {
            int posIndex = Random.Range(0, summonPoses.Length);

            float rangeX = Random.Range(-3f, 3f);
            float rangeZ = Random.Range(-3f, 3f);

            Vector3 pos = new Vector3(
                summonPoses[posIndex].position.x + rangeX,
                summonPoses[posIndex].position.y,
                summonPoses[posIndex].position.z + rangeZ
            );

            Instantiate(summonMonster, pos, Quaternion.identity);
            Instantiate(monsterSummonEffect, pos, Quaternion.identity);

        }
    }

    // 레이저 공격 이벤트
    public void LaserAttack()
    {
        // 레이캐스트 켜기
        onLaserAT = true;

        // 레이저 이펙트 시작
        laserPrefab.Play();

        battleAudio.clip = lazerAttackSound;
        battleAudio.Play();
    }

    // 레이저 공격 시작할때
    public void LaserAttackStart()
    {
        // 레이저 회전 속도
        slerpValue = 2;
    }

    // 레이저 공격 끝낼때
    public void LaserAttackEnd()
    {
        slerpValue = 20;
        onLaserAT = false;
    }

    // 레이저 충돌 오버랩
    public void OverlapCreate()
    {        

        Collider[] colliders = Physics.OverlapCapsule(shootPos.position, overlapEndPos.position, 0.5f);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                laserHitTime += Time.deltaTime;

                // 1초 간격으로 히트
                if (laserHitTime > 1f)
                {
                    collider.GetComponent<ObjectHealth>().Hit(1);
                    laserHitTime = 0;
                }
            }
        }
    }
}
