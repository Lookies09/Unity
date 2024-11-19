using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState_Skeleton_Ranger : EnemyAttackAbleState
{
    // ��ȸ ���� ����
    [SerializeField] protected float radius;

    // ��ȸ ��ġ ����
    protected Transform targetTransform = null;

    // ��ȸ ��ġ (�⺻ : ���� ��ġ��)
    public Vector3 targetPosition = Vector3.positiveInfinity;

    // ��ȸ�� �̵� �ӵ�
    [SerializeField] protected float moveSpeed;

    // ��ȸ ��ġ���� �Ÿ�
    public float targetDistance = Mathf.Infinity;

    // ��ȸ �̵� �׺���̼� üũ ���� �Ÿ�
    [SerializeField] protected float wanderNavCheckRadius;

    // ��ȸ ���� ��ġ ����Ʈ��
    [SerializeField] protected Transform[] wanderPoints;

    // ��ȸ ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ��ȸ �̵� �ӵ��� �׺���̼� ������Ʈ�� ����
        navMeshAgent.speed = moveSpeed;

        // ��ȸ �ִϸ��̼� ���
        animator.SetInteger("State", (int)state);

        // ���ο� ��ȸ ��ġ�� Ž��
        NewRandomDestination();
    }

    // ��ȸ ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // �̵� �������� ĳ���� ȸ��
        RotateCharacter(navMeshAgent.velocity.normalized);

        // �÷��̾ ���� ���� �Ÿ��ȿ� ������
        if (controller.GetPlayerDistance() <= attackDistance)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(2);
            return;
        }

        // ��ȸ�� �̵� ��ġ�� �����Ѵٸ�
        if (targetTransform != null)
        {
            // ��ȸ�� ��ġ ��ó�� �����ߴٸ�
            targetDistance = Vector3.Distance(transform.position, targetPosition);
            if (targetDistance < 0.5f)
            {
                // ��� ���·� ��ȯ
                controller.TransactionToState(0);

            }
        }
    }

    // ��ȸ ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {

        // �׺���̼� �̵� ����
        navMeshAgent.isStopped = true;

        // ��ȸ ���� ��ġ ������ �ʱ�ȭ
        targetTransform = null;
        targetPosition = Vector3.positiveInfinity;
        targetDistance = Mathf.Infinity;

    }

    // ���ο� ��ȸ ��ġ�� Ž����
    protected void NewRandomDestination()
    {
        // ��ȸ ��ġ �ε��� ��÷
        int index = Random.Range(0, wanderPoints.Length);

        // ���� ��ȸ ��ġ�� Ž�� �ߴٸ� �ٽ� Ž��
        float distance = Vector3.Distance(wanderPoints[index].position, transform.position);
        if (distance < radius)
        {
            // ��ȸ�� ��ġ�� �ٽ� ��÷��
            NewRandomDestination();
            return;
        }

        // ��ȸ ��ġ�� ����
        targetTransform = wanderPoints[index];

        // ��ȸ ��ġ�� ���������� ���� �������� ������ ��ġ�� �缱��
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += wanderPoints[index].position;
        randomDirection.y = gameObject.transform.position.y;

        // ���� ��÷�� ��ȸ ��ġ�� �׺���̼� ������Ʈ �̵� �ӵ��� ����
        targetPosition = randomDirection;

        Debug.Log($"��ȸ �̵��� ��ġ : {targetPosition}");

        // �׺���̼� �̵��� ��ȿ�ϴٸ�
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderNavCheckRadius, 1))
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.SetDestination(targetPosition);
        }
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
