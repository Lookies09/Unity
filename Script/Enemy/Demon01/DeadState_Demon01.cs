using INab.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_Demon01 : EnemyState
{
    [SerializeField] private Material burnMatrial;

    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    [SerializeField] private ScaleOscillate scaleOscillate;

    [SerializeField] private ParticleSystem[] smokes;

    // ��� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // �̵� ����
        navMeshAgent.isStopped = true;

        // ��� �ִϸ��̼� ���
        animator.SetBool("Dead", true);

        // ���Ͱ� �Ҹ��
        Invoke("objectDestroyEvent", 3);
    }

    // ��� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
    }

    // ��� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {

    }

    public void objectDestroyEvent()
    {
        foreach (ParticleSystem smoke in smokes)
        {
            smoke.Stop();
        }

        meshRenderer.material = burnMatrial;
        scaleOscillate.enabled = true;
        Destroy(gameObject, 10);
    }
}
