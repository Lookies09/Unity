using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : ObjectHealth
{
    // 플레이어 공격 스크립트
    private Player_Attack p_Attack;

    // 플레이어 움직임 스크립트
    private Player_Movement p_Movement;

    [SerializeField] private float stamina = 100;

    public float Stamina { get => stamina; set => stamina = value; }

    public override void Awake()
    {
        base.Awake();

        p_Attack = GetComponent<Player_Attack>();
        p_Movement = GetComponent<Player_Movement>();

    }

    private void Update()
    {
        animator.SetBool("OnHit", isHit);

        // 구르는 중이나 공격 중이 아니라면
        if (!p_Movement.IsDodge || !p_Attack.IsAttack) 
        {
            // 스테미너가 가득 찬 상태면 리턴
            if (stamina >= 100) { return; }
            // 초당 10씩 회복
            stamina += 10 * Time.deltaTime;
        }
    }

    public override void Death()
    {
        if (health <= 0)
        {
            isDead = true;
            animator.SetBool("Dead", true);
            return;
        }
    }

    public override void Hit(int DMG)
    {
        health -= DMG;

        Death();

        // 맞고있다면 리턴
        if (isHit)
        {
            return;
        }
        else
        {
            OnHit();
        }        
    }

    public void StaminaUse(int stamina)
    {
        this.Stamina -= stamina;
    }
}
