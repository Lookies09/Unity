using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Demon02 : EnemyAttackAbleState
{
    // ��Ʋ ����� (���� �Ҹ�)
    [SerializeField] private AudioSource battleAudio;

    // ����Ʈ ���� ����� (�� ��� �Ҹ�)
    [SerializeField] private AudioSource effectAudio;

    // �׶��� ��Ʈ ����Ʈ
    [SerializeField] private ParticleSystem groundHitEffect;

    // ��Ҹ� ������ҽ�
    [SerializeField] private AudioSource voiceAudio;

    // ���� ��Ҹ� Ŭ��
    [SerializeField] private AudioClip[] attackvoices;

    // �� ��Ʈ ����� Ŭ��
    [SerializeField] private AudioClip groundHitSound;

    // �ð�
    private float time;

    // ���� ���� �ð�
    private float ADelaytime = 0;

    // ���� ��Ʈ�ѷ�
    private AttackController attackController;

    // ���� ���� Ÿ��
    private int ranAttackInt;

    // ���� ���� Ÿ�� ���
    private int beforeAtInt = 0;


    public override void Awake()
    {
        base.Awake();

        attackController = GetComponent<AttackController>();
    }

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
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
        if (ranAttackInt == 0) { ADelaytime = 2f; } // �޺� ����
        else if (ranAttackInt == 1) { ADelaytime = 2f; } // �Ѽ� ����
        else if (ranAttackInt == 2) { ADelaytime = 3.5f; } // ���� ����


        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", state);

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
                // 1 ������ ���� �� �ȱ�
                if (GetComponent<ObjectHealth>().Phase == 0)
                {
                    // �� �ȱ�
                    controller.TransactionToState(3);
                    return;
                }
                else // 2 ������ ���� �߰�
                {
                    // �߰�
                    controller.TransactionToState(2);
                    return;
                }
                

            }
            else
            {
                // ����
                controller.TransactionToState(4);
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
        effectAudio.clip = groundHitSound;
        effectAudio.spatialBlend = 1;
        groundHitEffect.Play();
        effectAudio.Play();
    }
}
