using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT2_AT_Darklord : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 2�� ���� ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
