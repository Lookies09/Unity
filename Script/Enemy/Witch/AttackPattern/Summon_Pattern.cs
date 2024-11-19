using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 소환 애니메이션 재생
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
