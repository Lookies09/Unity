using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONEHAND_AT_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // �Ѽ� ���� �ִϸ��̼� ���
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
