using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Witch : EnemyAttackAbleState
{
    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // ��� �ִϸ��̼� ���
        animator.SetInteger("State", state);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // �÷��̰� �߰� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            // ���� ����
            controller.TransactionToState(1);
            return;
        }
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        
    }
}