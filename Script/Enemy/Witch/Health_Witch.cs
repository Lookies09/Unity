using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Witch : ObjectHealth
{
    private EnemyController controller;       

    // ��� ī��Ʈ
    private int countD = 0;    

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
        Death();

        // ���� ü���� 60���� ���϶��
        if (health < fisrtHealth * 0.6f && Phase == 0)
        {
            // ��� ���·� ��ȯ�ϰ�
            controller.TransactionToState(4);
            // ������ �ø���
            Phase++;
        }
    }

    public override void Death()
    {
        if (health <= 0 && countD == 0)
        {            
            IsDead = true;
            controller.TransactionToState(6);
            gameObject.GetComponent<Collider>().enabled = false;
            countD++;
            return;
        }

    }

    public override void Hit(int DMG)
    {
        // ��� ���°ų� ������ ���
        if (controller.CurrentState == controller.EnemyStates1[4] ||
            controller.CurrentState == controller.EnemyStates1[0])
        {
            return;
        }
        else
        {
            health -= DMG;

            Death();

            // �°��ִٸ� ����
            if (isHit)
            {
                return;
            }
            else
            {
                // �´� ��Ҹ�
                GetComponent<FSMController_Witch>().OnHitVoice();
                OnHit();
                controller.TransactionToState(5);
            }
        }        
    }
}
