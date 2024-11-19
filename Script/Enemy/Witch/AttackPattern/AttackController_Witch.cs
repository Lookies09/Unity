using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController_Witch : AttackController
{
    // 마녀 공격 패턴들
    // 1 페이즈는 AREA_AT 까지
    public enum Attack { H1_AT, H2_AT, AREA_AT, SUMMON, LASER}
}
