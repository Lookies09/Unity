using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // ��ȯ �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
