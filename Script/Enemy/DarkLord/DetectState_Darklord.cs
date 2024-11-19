using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectState_Darklord : EnemyAttackAbleState
{
    // ���� �̵� �ӵ�
    [SerializeField] protected float detectSpeed;

    // �߰� �ð�
    private float time;

    private bool onAvoid = false;

    // �߰� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        time = 0;

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
        time += Time.deltaTime;

        navMeshAgent.SetDestination(controller.Player.transform.position);

        // �̵� �������� ĳ���� ȸ��
        RotateCharacter(navMeshAgent.velocity.normalized);


        // �÷��̰� ���� ���� �Ÿ��ȿ� ���Դٸ�
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ����
            controller.TransactionToState(3);
            return;
        }

        if (onAvoid) 
        {
            // ȸ�� ����
            controller.TransactionToState(4);
        }

        animator.SetFloat("WalkTime", time);
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        onAvoid = false;
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Bullet" && controller.CurrentState == controller.EnemyStates1[2])
        {
            onAvoid = true;
        }
    }
}
