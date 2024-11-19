using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Demon02 : ObjectHealth
{
    private GameObject Demon01;

    private EnemyController controller;

    // 사망 카운트
    private int countD = 0;


    private void Start()
    {
        Demon01 = GetComponent<FSMController_Demon02>().Demon01;
    }

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
        Death();

        
        // 만약 다른 악마가 죽었다면
        if (Demon01 != null && Demon01.GetComponent<ObjectHealth>().IsDead && Phase == 0)
        {
            // 페이즈 상승
            Phase = 1;
        }
    }

    public override void Death()
    {
        if (health <= 0 && countD == 0)
        {
            isDead = true;
            controller.TransactionToState(5);
            gameObject.GetComponent<Collider>().enabled = false;
            countD++;
            return;
        }

    }

    public override void Hit(int DMG)
    {
        health -= DMG;
    }
}
