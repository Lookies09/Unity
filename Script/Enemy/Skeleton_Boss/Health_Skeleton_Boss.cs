using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Skeleton_Boss : ObjectHealth
{
    private EnemyController controller;

    public override void Awake()
    {
        base.Awake();
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
       
    }

    public override void Death()
    {
        if (health <= 0)
        {
            isDead = true;
            controller.TransactionToState(4);
            gameObject.GetComponent<Collider>().enabled = false;
        }

    }

    public override void Hit(int DMG)
    {
        health -= DMG;

        Death();
    }
}
