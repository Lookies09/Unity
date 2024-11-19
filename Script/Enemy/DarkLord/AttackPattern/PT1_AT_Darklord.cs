using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PT1_AT_Darklord : AttackPatternState
{
    public override void EnterState(int state)
    {
        // 1번 패턴 공격 애니메이션 재생
        animator.SetInteger("AttackPattern", state);


        // 좌우 대쉬 방향 결정 (0 : 좌, 1 : 우)
        int dirction = Random.Range(0, 2);

        animator.SetInteger("SlideDirc", dirction);
    }


    public override void ExitState()
    {

    }
}
