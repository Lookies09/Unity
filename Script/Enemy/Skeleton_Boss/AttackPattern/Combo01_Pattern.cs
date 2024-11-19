using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo01_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 콤보1 공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
