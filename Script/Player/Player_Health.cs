using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : ObjectHealth
{
    // �÷��̾� ���� ��ũ��Ʈ
    private Player_Attack p_Attack;

    // �÷��̾� ������ ��ũ��Ʈ
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

        // ������ ���̳� ���� ���� �ƴ϶��
        if (!p_Movement.IsDodge || !p_Attack.IsAttack) 
        {
            // ���׹̳ʰ� ���� �� ���¸� ����
            if (stamina >= 100) { return; }
            // �ʴ� 10�� ȸ��
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

        // �°��ִٸ� ����
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
