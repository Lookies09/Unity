using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class JumpDownState_Gargoyle : EnemyAttackAbleState
{
    [SerializeField] private Animator wingAnimtor;

    [SerializeField] private ParticleSystem areaEffect;

    // �� ���� ����Ʈ
    [SerializeField] private ParticleSystem groundHitEffect;

    // ī�޶� ����ũ
    [SerializeField] private GameObject cameraShake;

    // �����ٵ�
    private Rigidbody rb;

    // ���� �ð�
    private float time;

    // ���� �浹
    private bool onGround = false;

    // ���� Ȯ��
    private bool onDown = false;

    // ===================================================================

    // ���� ���� ��� ���̾�
    [SerializeField] private LayerMask targetLayer;

    // ���� ���� ����
    [SerializeField] private float attackRadius;


    public override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    // ���� ���� ����(����) ó��(���� �ʱ�ȭ)
    public override void EnterState(int state)
    {
        // ���� �ִϸ��̼� ���
        animator.SetInteger("State", state);

        // ������ Ȱ��ȭ
        wingAnimtor.enabled = true;

        navMeshAgent.enabled = false;
        animator.applyRootMotion = false;                
    }

    // ���� ���� ��� ���� ó�� (���� ����)
    public override void UpdateState()
    {
        time += Time.deltaTime;

        if (time <= 3f && time > 0.5f && !onGround)
        {
            Vector3 pos = controller.Player.transform.position;
            pos.y = transform.position.y;

            if (Vector3.Distance(transform.position, pos) > 3)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, 10 * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(pos - transform.position, Vector3.up);


            }
            else
            {

                transform.rotation = transform.rotation;
            }
        }

        if (time > 3f && !onGround) // 3�� �����ٸ�
        {            
            rb.velocity = Vector3.down * 100;
            onDown = true;
        }

        if (onGround && time > 1.5f)
        {
            // �÷��̰� �߰� ���� �Ÿ��ȿ� ���Դٸ�
            if (controller.GetPlayerDistance() <= detectDistance)
            {
                // ���� ����
                controller.TransactionToState(2);
                return;
            }

            // ���ݰ��� �Ÿ��ȿ� ���Դٸ�
            if (controller.GetPlayerDistance() <= attackDistance)
            {
                // ���� ����
                controller.TransactionToState(3);
            }
        }


        animator.SetFloat("DropTime", time);
        animator.SetBool("OnDown", onDown);
    }

    // ���� ���� ����(�ٸ����·� ����) ���� ó��(���� ����)
    public override void ExitState()
    {
        time = 0;
        onGround = false;
        onDown = false;
        animator.SetBool("OnGround", onGround);

        // ������ Ȱ��ȭ
        wingAnimtor.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enviroment" && time > 2f)
        {
            // ����, ��Ʈ ����Ʈ, ī�޶���ũ ���
            areaEffect.Play();
            groundHitEffect.Play();

            LandingAttack();

            Instantiate(cameraShake, transform.position, Quaternion.identity);
            GetComponent<FSMController_Gargoyle>().DownSound();

            // ������ ��Ȱ��ȭ
            wingAnimtor.enabled = false;
            onGround = true;
            navMeshAgent.enabled = true;
            animator.SetBool("OnGround", onGround);
            time = 0;

            if (navMeshAgent.enabled == true)
            {
                animator.applyRootMotion = true;
            }
        }
    }

    public void JumpUp()
    {
        GetComponent<FSMController_Gargoyle>().JumpSound();
        onDown = false;
        rb.velocity = Vector3.up * 30;
        
    }

    public void LandingAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, targetLayer);

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
}
