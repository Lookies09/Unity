using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState_NormalSkeleton : EnemyAttackAbleState
{
    // �ǰ� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
    }

    // �ǰ� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // �°������� ����
        if (GetComponent<ObjectHealth>().IsHit)
        {
            return;
        }
        

        // �÷��̰� �߰� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            // ���� ����
            controller.TransactionToState(2);
            return;
        }

        // ���ݰ��� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ����
            controller.TransactionToState(3);
        }
    }

    // �ǰ� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
    }
}
