using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Skeleton_LightArmor : ObjectHealth
{
    private EnemyController controller;

    private int deadCount = 0;
        

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
        if (health <= 0 && deadCount == 0)
        {
            controller.TransactionToState(5);
            gameObject.GetComponent<Collider>().enabled = false;
            deadCount++;
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
