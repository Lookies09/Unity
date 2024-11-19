using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Witch : EnemyAttackAbleState
{
    [SerializeField] private AudioSource battleAudio;

    // 0�� - ��������, 1�� - �߰ݰ��� �Ҹ�
    [SerializeField] private AudioClip[] normalAttackSounds;

    // ���� ���� ����
    [SerializeField] private AudioClip roundAttackSound;

    // ��ȯ ���� ����
    [SerializeField] private AudioClip summonAttackSound;

    // ������ ���� ����
    [SerializeField] private AudioClip lazerAttackSound;

    // �ð�
    private float time;

    // ������ ��Ʈ �ð�
    private float laserHitTime;

    // ���� ���� Ÿ��
    private int ranAttackInt;

    // ���� ���� �ð�
    private float ADelaytime = 0;

    // ���� ���� Ÿ�� ���
    private int beforeAtInt = 0;

    // ���� ��Ʈ�ѷ�
    private AttackController attackController;

    // �⺻ ����ü �߻� ��ġ
    [SerializeField] private Transform shootPos;

    // ���� ����ü �߻� ��ġ��
    [SerializeField] private Transform[] shootPoses;

    // ������ ������ ��ġ
    [SerializeField] private Transform overlapEndPos;

    // �⺻ ����ü
    [SerializeField] private GameObject bulletPrefab;

    // �߰� ����ü
    [SerializeField] private GameObject traceBullet;

    // ���� ����Ʈ
    [SerializeField] private ParticleSystem areaEffect;

    // ��ȯ ���̷��� ������
    [SerializeField] private GameObject summonMonster;

    // ��ȯ ����Ʈ
    [SerializeField] private ParticleSystem summonEffect;

    // ��ȯ�Ҷ� ���� �ؿ� ����� ����Ʈ
    [SerializeField] private GameObject monsterSummonEffect;

    // ��ȯ ��ġ��
    [SerializeField] private Transform[] summonPoses;

    // ������ ������
    [SerializeField] private ParticleSystem laserPrefab;

    // ������ �ѷ�
    [SerializeField] private float maxRayLength;

    // ������ ���� ���� Ȯ��
    private bool onLaserAT;

    public override void Awake()
    {
        base.Awake();

        attackController = GetComponent<AttackController>();
    }

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", state);

        // ������ ���� �̵� ����
        navMeshAgent.isStopped = true;

        // ���ݽ� ��Ʈ �ִϸ��̼����� �ȳѾ��
        GetComponent<ObjectHealth>().IsHit = true;

        // 1��������
        if (gameObject.GetComponent<Health_Witch>().Phase == 0)
        {
            while (true)
            {
                // ���� ���� ���� ��÷
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
                // ���� ���� ���� ��÷
                ranAttackInt = Random.Range(0, attackController.attackPatternState1.Length);

                if (beforeAtInt != ranAttackInt)
                {
                    beforeAtInt = ranAttackInt;
                    break;
                }

            }
        }
        

        // ���� Ÿ�Ժ� �ð� �Է�
        if (ranAttackInt == 0) { ADelaytime = 1.3f; } // �Ѽ� ����
        else if (ranAttackInt == 1) { ADelaytime = 1.5f; } // ��� ����
        else if (ranAttackInt == 2) { ADelaytime = 3f; } // ���� ����
        else if (ranAttackInt == 3) { ADelaytime = 3.5f; } // ��ȯ
        else { ADelaytime = 5f; } // ������ ����

        // ���� ��Ʈ�ѷ����� ���� ���� ����
        attackController.TransactionToState(ranAttackInt);
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        LookAtTarget(true);

        if (ADelaytime < time)
        {
            // ���� ����� ���� ���� �Ÿ����� �־����ٸ�
            if (controller.GetPlayerDistance() > attackDistance)
            {

                // �߰�
                controller.TransactionToState(2);
                return;

            }
            else
            {
                // ����
                controller.TransactionToState(3);
            }


        }

        if (onLaserAT)
        {
            OverlapCreate();
        }
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
        ADelaytime = 0;
        ranAttackInt = 0;
        GetComponent<ObjectHealth>().IsHit = false;
    }

    // �⺻ ����
    public void Shoot()
    {
        battleAudio.clip = normalAttackSounds[0];
        battleAudio.Play();
        Instantiate(bulletPrefab, shootPos.position, shootPos.rotation);
    }

    // �߰� ����
    public void TraceShoot()
    {
        battleAudio.clip = normalAttackSounds[1];
        battleAudio.Play();
        Instantiate(traceBullet, shootPos.position, Quaternion.identity);
    }

    // ���� �߻� ����
    public void AreaShoot()
    {
        areaEffect.Play();
        // 1 �������
        if (gameObject.GetComponent<Health_Witch>().Phase == 0)
        {
            // 10�������� �߻�
            for (int i = 0; i < shootPoses.Length-10; i++)
            {
                Instantiate(bulletPrefab, shootPoses[i].position, shootPoses[i].rotation);
            }
        }
        else // 2�������
        {
            // ��ü �߻�
            for (int i = 0; i < shootPoses.Length; i++)
            {
                Instantiate(bulletPrefab, shootPoses[i].position, shootPoses[i].rotation);
            }
        }

        battleAudio.clip = roundAttackSound;
        battleAudio.Play();

    }

    // ��ȯ �̺�Ʈ
    public void Summon()
    {
        summonEffect.Play();

        battleAudio.clip = summonAttackSound;
        battleAudio.Play();

        // ��� ��ȯ? ������ 6����
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

    // ������ ���� �̺�Ʈ
    public void LaserAttack()
    {
        // ����ĳ��Ʈ �ѱ�
        onLaserAT = true;

        // ������ ����Ʈ ����
        laserPrefab.Play();

        battleAudio.clip = lazerAttackSound;
        battleAudio.Play();
    }

    // ������ ���� �����Ҷ�
    public void LaserAttackStart()
    {
        // ������ ȸ�� �ӵ�
        slerpValue = 2;
    }

    // ������ ���� ������
    public void LaserAttackEnd()
    {
        slerpValue = 20;
        onLaserAT = false;
    }

    // ������ �浹 ������
    public void OverlapCreate()
    {        

        Collider[] colliders = Physics.OverlapCapsule(shootPos.position, overlapEndPos.position, 0.5f);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                laserHitTime += Time.deltaTime;

                // 1�� �������� ��Ʈ
                if (laserHitTime > 1f)
                {
                    collider.GetComponent<ObjectHealth>().Hit(1);
                    laserHitTime = 0;
                }
            }
        }
    }
}
