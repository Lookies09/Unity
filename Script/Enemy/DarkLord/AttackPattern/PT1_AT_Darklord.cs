using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT1_AT_Darklord : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 1�� ���� ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);


        // �¿� �뽬 ���� ���� (0 : ��, 1 : ��)
        int dirction = Random.Range(0, 2);

        animator.SetInteger("SlideDirc", dirction);
    }


    public override void ExitState()
    {

    }
}
