using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_Skeleton_Flesh : EnemyState
{
    // ���� ����Ʈ
    [SerializeField] private GameObject explosionEffect;
    // ���� ��ġ
    [SerializeField] private Transform explosionPos;

    // ���� ��� ���̾�
    [SerializeField] private LayerMask targetLayer;

    // ���� ����
    [SerializeField] private float explosionRadius;

    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �̵� ����
        navMeshAgent.isStopped = true;

        // ���⼭ ����Ʈ ��Ʈ���鼭 ����
        Instantiate(explosionEffect, explosionPos.position, Quaternion.identity);

        ExplosionAttack();
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        // ���Ͱ� �Ҹ��
        Destroy(gameObject, 0.1f);
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {

    }

    public void ExplosionAttack()
    {
        Collider[] hits = Physics.OverlapSphere(explosionPos.position, explosionRadius, targetLayer);

        foreach (Collider hit in hits)
        {
            if (hit.tag == "Player")
            {
                Debug.Log("���� ������ ����");

                hit.gameObject.GetComponent<ObjectHealth>().Hit(10);
                break;
            }

        }
    }

    public void OnDrawGizmosSelected()
    {
        // ���� �ݰ��� �ð������� ǥ��
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(explosionPos.position, explosionRadius);
    }
}
