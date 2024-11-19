using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Demon01 : ObjectHealth
{
    [SerializeField] private GameObject Demon02;

    private EnemyController controller;

    // 사망 카운트
    private int countD = 0;

    private void Start()
    {
        Demon02 = GetComponent<FSMController_Demon01>().Demon02;
    }

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
        Death();

        // 만약 체력이 60프로 이하라면
        if (health < fisrtHealth * 0.6f && Phase != 1)
        {
            // 악마 2 부르기
            Phase = 1;
        }

        // 만약 다른 악마가 죽었다면
        if (Demon02.GetComponent<ObjectHealth>().IsDead && Phase != 2)
        {
            Phase = 2;
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
