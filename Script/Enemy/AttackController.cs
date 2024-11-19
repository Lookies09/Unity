using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{


    // ���� ���� �������� ���� ���� ������Ʈ
    protected AttackPatternState currentState;

    // ���� ��� ���� ���� ������Ʈ��
    [SerializeField] protected AttackPatternState[] attackPatternState;

    protected Animator animator;

    public AttackPatternState CurrentState { get => currentState; set => currentState = value; }
    public AttackPatternState[] attackPatternState1 { get => attackPatternState; set => attackPatternState = value; }

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }
            

    public void TransactionToState(int state)
    {
        
        CurrentState?.ExitState(); // ���� ���� ����
        CurrentState = attackPatternState[state]; // ���� ��ȯ ó��
        CurrentState.EnterState(state); // ���ο� ���� ����
    }

}
