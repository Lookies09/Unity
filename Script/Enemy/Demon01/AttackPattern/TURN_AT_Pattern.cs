using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TURN_AT_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // ȸ�� ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
