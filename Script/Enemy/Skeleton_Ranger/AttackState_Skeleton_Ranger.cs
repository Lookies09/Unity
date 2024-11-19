using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_Skeleton_Ranger : EnemyAttackAbleState
{
    // �ð�
    private float time;

    // �Ѿ� ������
    [SerializeField] private GameObject bulletPrefab;

    // �߻� ��ġ��
    [SerializeField] private Transform[] shootPoses;

    // ���� �����
    [SerializeField] private AudioSource attackAudio;

    

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
        if (time > 3f)
        {
            // ���� ����� ���� ���� �Ÿ����� �־����ٸ�
            if (controller.GetPlayerDistance() > attackDistance)
            {

                // �߰�
                controller.TransactionToState(2);
                return;

            }
            // ���� ����� ���� ���� �Ÿ��� �ִٸ�
            else if (controller.GetPlayerDistance() <= attackDistance)
            {
                // ���
                controller.TransactionToState(0);
            }
        }
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
    }

    // ���� �̺�Ʈ,
    public void Shoot()
    {
        for (int i = 0; i < shootPoses.Length; i++)
        {
            Instantiate(bulletPrefab, shootPoses[i].position, shootPoses[i].rotation);
        }
    }

    public void AttackAudioPlay()
    {
        attackAudio.Play();
    }

}
