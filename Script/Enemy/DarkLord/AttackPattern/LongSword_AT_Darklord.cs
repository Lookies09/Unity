using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword_AT_Darklord : AttackPatternState
{
    public override void EnterState(int state)
    {
        // �� Į ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
