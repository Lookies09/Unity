using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo01_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // �޺�1 ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
