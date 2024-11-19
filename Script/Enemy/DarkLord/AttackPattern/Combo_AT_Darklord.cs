using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo_AT_Darklord : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 콤보 공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);

    }


    public override void ExitState()
    {

    }
}
