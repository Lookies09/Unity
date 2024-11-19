using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUMPTURN_AT_Pattern : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 점프 턴 공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
