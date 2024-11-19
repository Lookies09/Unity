using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Darklord : ObjectHealth
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
