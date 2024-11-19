using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Skeleton_Flesh : ObjectHealth
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
            controller.TransactionToState(2);
            gameObject.GetComponent<Collider>().enabled = false;
        }

    }

    public override void Hit(int DMG)
    {
        health -= DMG;
        Death();
    }
}
