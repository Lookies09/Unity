using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectHealth : MonoBehaviour
{
    // ĳ���� ü��
    [SerializeField] protected int health;

    // �ʱ� ü��
    protected int fisrtHealth;

    // �ִϸ����� ����
    protected Animator animator;

    // ĳ���� ���� �Ǻ�
    protected bool isDeath = false;

    // ĳ���� �ǰ� �Ǻ�
    protected bool isHit = false;

    // ��� Ȯ��
    protected bool isDead = false;

    // ������
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
