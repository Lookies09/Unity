using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_AT_Darklord : AttackPatternState
{
    public override void EnterState(int state)
    {
        // ���� ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}