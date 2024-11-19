using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Witch : ObjectHealth
{
    private EnemyController controller;       

    // 사망 카운트
    private int countD = 0;    

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
        Death();

        // 만약 체력이 60프로 이하라면
        if (health < fisrtHealth * 0.6f && Phase == 0)
        {
            // 방어 상태로 전환하고
            controller.TransactionToState(4);
            // 페이즈 올리기
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
        // 방어 상태거나 대기상태 라면
        if (controller.CurrentState == controller.EnemyStates1[4] ||
            controller.CurrentState == controller.EnemyStates1[0])
        {
            return;
        }
        else
        {
            health -= DMG;

            Death();

            // 맞고있다면 리턴
            if (isHit)
            {
                return;
            }
            else
            {
                // 맞는 목소리
                GetComponent<FSMController_Witch>().OnHitVoice();
                OnHit();
                controller.TransactionToState(5);
            }
        }        
    }
}
