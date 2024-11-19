using INab.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_Witch : EnemyState
{
    [SerializeField] private ScaleOscillate scaleOscillate;

    [SerializeField] private ParticleSystem[] particles;

    [SerializeField] private Light p_Light;

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
        foreach (ParticleSystem particle in particles) 
        {
            particle.Stop();
        }

        p_Light.enabled = false;
        scaleOscillate.enabled = true;
        Destroy(gameObject, 10);
    }
}
