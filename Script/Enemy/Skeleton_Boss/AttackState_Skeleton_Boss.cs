using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Skeleton_Boss : EnemyAttackAbleState
{
    // �ð�
    private float time;

    // ���� ���� Ÿ��
    private int ranAttackInt;

    // ���� ���� Ÿ�� ���
    private int beforeAtInt = -1;

    // ���� ���� �ð�
    private float ADelaytime = 0;

    // ���� ��Ʈ�ѷ�
    private AttackController attackController;

    // ����ü �߻� ��ġ��
    [SerializeField ] private Transform[] shootPoses;

    // ����ü
    [SerializeField] private GameObject bulletPrefab;

    // ����ü ����
    [SerializeField] private GameObject verticalBullet;

    // ���� �����
    [SerializeField] private AudioSource battleAudio;

    [SerializeField] private AudioClip[] attackClips;

    // ����Ʈ �����
    [SerializeField] private AudioSource effectAudio;


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

        // ���� Ÿ�Ժ� �ð� �Է�
        if (ranAttackInt == 0) { ADelaytime = 2.5f; } // �޺� 1 ����
        else if (ranAttackInt == 1) { ADelaytime = 2f; } // �⺻ ����

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
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
        ADelaytime = 0;
        ranAttackInt = 0;
    }

    // ���� �̺�Ʈ,
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
