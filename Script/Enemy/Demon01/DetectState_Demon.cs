using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectState_Demon : EnemyAttackAbleState
{
    // ���� �̵� �ӵ�
    [SerializeField] protected float detectSpeed;

    // �߰� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", state);

        // ���� ��� ���� ó��
        navMeshAgent.isStopped = false;
        navMeshAgent.updateRotation = false;

        // ���� �ӵ� ����
        navMeshAgent.speed = detectSpeed;
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        navMeshAgent.SetDestination(controller.Player.transform.position);

        // �̵� �������� ĳ���� ȸ��
        RotateCharacter(navMeshAgent.velocity.normalized);


        // �÷��̰� ���� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() < attackDistance)
        {
            // ���� ����
            controller.TransactionToState(4);
            return;
        }
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
    }

    // ĳ���� �̵� �������� ȸ��
    private void RotateCharacter(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // ȸ�� �ӵ��� ������ �� ����
        }
    }
}