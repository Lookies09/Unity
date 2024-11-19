using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_NormalSkeleton : ObjectHealth
{
    private EnemyController controller;

    private int countD = 0;

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
    }    

    private void Update()
    {
        Death();
    }

    public override void Death()
    {
        if (health <= 0 && countD == 0)
        {            
            isDead = true;
            controller.TransactionToState(5);
            gameObject.GetComponent<Collider>().enabled = false;
            countD++;
        }
            
    }

    public override void Hit(int DMG)
    {      

        health -= DMG;

        

        // 맞고있다면 리턴
        if (isHit)
        {
            return;
        }
        else
        {
            OnHit();
            controller.TransactionToState(4);
        }        
    }

}
