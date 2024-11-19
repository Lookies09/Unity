using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectHealth : MonoBehaviour
{
    // 캐릭터 체력
    [SerializeField] protected int health;

    // 초기 체력
    protected int fisrtHealth;

    // 애니메이터 참조
    protected Animator animator;

    // 캐릭터 죽음 판별
    protected bool isDeath = false;

    // 캐릭터 피격 판별
    protected bool isHit = false;

    // 사망 확인
    protected bool isDead = false;

    // 페이즈
    private int phase = 0;
    public int Phase { get => phase; set => phase = value; }

    public int Health { get => health; set => health = value; }
    public bool IsHit { get => isHit; set => isHit = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public int FisrtHealth { get => fisrtHealth; set => fisrtHealth = value; }

    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        FisrtHealth = health;
    }

    public abstract void Hit(int DMG);

    public abstract void Death();


    protected void OnHit()
    {
        IsHit = true;
    }

    protected void ExitHit()
    {
        IsHit = false;
    }
    
}
