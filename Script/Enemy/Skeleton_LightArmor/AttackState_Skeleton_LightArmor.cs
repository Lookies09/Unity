using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Skeleton_LightArmor : EnemyAttackAbleState
{
    // �ð�
    private float time;

    // ���� �����
    [SerializeField] private AudioSource attackAudio;

    [SerializeField] private AudioClip[] attackClips;

    

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);

        // ������ ���� �̵� ����
        navMeshAgent.isStopped = true;
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;
        LookAtTarget(true);

        // ���� ��ǿ� ���� 3�ʰ� ������
        if (time > 1.5f)
        {
            // ���� ����� ���� ���� �Ÿ����� �־����ٸ�
            if (controller.GetPlayerDistance() > attackDistance)
            {

                // �߰�
                controller.TransactionToState(2);
                return;

            }
        }
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
    }

    public void AttackAudioPlay()
    {
        int num = Random.Range(0, attackClips.Length);

        attackAudio.clip = attackClips[num];

        attackAudio.Play();
    }
}
