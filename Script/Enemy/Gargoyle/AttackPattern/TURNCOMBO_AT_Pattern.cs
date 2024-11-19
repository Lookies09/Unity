using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TURNCOMBO_AT_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 회전 콤보 공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
