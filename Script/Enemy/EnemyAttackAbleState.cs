using UnityEngine;

public class EnemyAttackAbleState : EnemyState
{
    [SerializeField] protected float attackDistance; // �÷��̾� ���� ���� �Ÿ�
    [SerializeField] protected float detectDistance; // �÷��̾� ���� ���� �Ÿ�
    [SerializeField] protected float slerpValue = 20;

    public override void EnterState(int state)
    {
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    // ���� ����� �ֽ�
    public void LookAtTarget(bool isOn)
    {
        if (isOn)
        {
            // ���� ����� ���� ������ ���
            Vector3 direction = (GameObject.Find("Player").transform.position - transform.position).normalized;

            // ȸ�� ���ʹϾ� ���
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

            // ���� ȸ��
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, slerpValue * Time.deltaTime);
        }
    }
}
