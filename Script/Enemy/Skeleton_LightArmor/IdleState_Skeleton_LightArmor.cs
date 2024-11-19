using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_Skeleton_LightArmor : EnemyAttackAbleState
{
    [SerializeField] protected float time; // �ð� ����
    [SerializeField] protected float checkTime; // ��� üũ �ð�
    [SerializeField] protected Vector2 checkTimeRange; // ��� üũ �ð� (�ּ�,�ִ�)

    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ���� üũ �ֱ� �ð��� ��÷��
        time = 0;
        checkTime = Random.Range(checkTimeRange.x, checkTimeRange.y);

        // �׺���̼� ������Ʈ �̵� ����
        navMeshAgent.isStopped = true;

        // ��� �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime; // ��� �ð� ���

        // �÷��̾ ���� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(3);
            return;
        }

        // �÷��̾ �߰� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= detectDistance)
        {
            // �߰� ���·� ��ȯ
            controller.TransactionToState(2);
            return;
        }

        // ��� ���°� �����ٸ�
        if (time > checkTime)
        {
            // ��ȸ ���·� ��ȯ
            controller.TransactionToState(1);
            return;
        }
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
    }
}
