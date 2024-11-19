using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ��Ʈ�ѷ� �θ�
public abstract class EnemyController : MonoBehaviour
{
    // ���� ����
    
    // ���� ���� �������� ���� ������Ʈ
    [SerializeField] protected EnemyState currentState;

    // ���� ��� ���� ������Ʈ��
    [SerializeField] protected EnemyState[] EnemyStates;

    // �÷��̾� ����
    protected GameObject player;    

    // �ִϸ�����
    protected Animator animator;

    public GameObject Player { get => player; set => player = value; }
    public EnemyState[] EnemyStates1 { get => this.EnemyStates; set => this.EnemyStates = value; }
    public EnemyState CurrentState { get => currentState; set => currentState = value; }


    protected void Awake()
    {
        animator = GetComponent<Animator>();        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Player = GameObject.FindWithTag("Player");

        // ��� ���·� ����
        TransactionToState(0);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // * ���� ������ ������ ����� ����!
        CurrentState?.UpdateState();
    }

    // * ���� ��ȯ �޼ҵ�
    public void TransactionToState(int state)
    {
        Debug.Log($"���� ���� ��ȯ : {gameObject.name}�� ���� : {state}");

        CurrentState?.ExitState(); // ���� ���� ����
        CurrentState = EnemyStates[state]; // ���� ��ȯ ó��
        CurrentState.EnterState(state); // ���ο� ���� ����
    }

    // �÷��̾�� ���� ���� �Ÿ� ����
    public float GetPlayerDistance()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }    

}
