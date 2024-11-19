using INab.Demo;
using INab.WorldAlchemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_Skeleton_Boss : EnemyState
{
    [SerializeField] private ScaleOscillate scaleOscillate;

    [SerializeField] private Light[] lights;

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

        foreach (Light light in lights)
        {
            light.enabled = false;
        }

        scaleOscillate.enabled = true;
        Destroy(gameObject,10);
    }
}
