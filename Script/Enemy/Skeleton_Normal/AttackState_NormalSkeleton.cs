using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState_NormalSkeleton : EnemyAttackAbleState
{
    // ���� �ð�
    private float aTime;

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", state);
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        aTime += Time.deltaTime;

        LookAtTarget(true);

        // 1.5�ʰ� ������
        if (aTime > 1.5f)
        {
            // �÷��̾ ���� ���ɰŸ����� �־�����
            if (controller.GetPlayerDistance() > attackDistance)
            {
                // �߰� ����
                controller.TransactionToState(2);
                return;
            }
            else
            {
                // ���� ����
                controller.TransactionToState(3);
                return;
            }
        }

        
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        aTime = 0;
    }
}
