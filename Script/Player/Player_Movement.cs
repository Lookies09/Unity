using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    // �̵��ӵ�
    [SerializeField] private float moveSpeed;

    // ĳ���� ��Ʈ�ѷ�
    private CharacterController cc;

    // �÷��̾� ���� ����
    private Player_Health playerState;

    //�ִϸ�����
    private Animator animator;

    //�̵� ����
    private Vector3 direction;

    // ���� Ȯ��
    private bool isMoving = false;

    // �������� Ȯ��
    private bool isDodge;

    // ������ �ð�
    private float moveTime;

    public bool IsDodge { get => isDodge; set => isDodge = value; }

    private void Awake()
    {
        cc = gameObject.GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerState = GetComponent<Player_Health>();
    }


    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        direction = new Vector3(h, 0, v);

        // ī�޶� y���� yȸ�������� �������� ĳ������ �ü� ���⺤�͸� ����
        direction = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * direction;
        direction.Normalize(); // ���⺤�� ����ȭ��


        // ���� * �ӵ�
        direction = direction.normalized * moveSpeed;

        // �¾Ұų� �׾����� ����
        if (GetComponent<Player_Health>().IsHit || GetComponent<Player_Health>().IsDead)
        {
            return;
        }

        // �̵��ϰ� �ִٸ�
        if (direction.magnitude > 0.1f && GetComponent<Player_Attack>().IsAttack == false)
        {
            moveTime += Time.deltaTime;

            isMoving = true;            

            // �ε巴�� ȸ��
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), 720 * Time.deltaTime);
            
            // ĳ���� ��Ʈ�ѷ� �̵�
            cc.Move(direction * Time.deltaTime);

            // �ִϸ����� ����
            animator.SetFloat("Movement", direction.magnitude);            
        }
        // ���߾��ٸ�
        else 
        {
            // ���� �ִϸ��̼� �÷���
            if (isMoving)
            {
                animator.SetTrigger("StopMove");
                isMoving = false;
            }

            moveTime = 0;

            // �ִϸ����� ����
            animator.SetFloat("Movement", 0f);
        }

        // �뽬 ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //  ���� ���׹̳ʰ� ��� ���¹̳� ���� �۴ٸ� ����
            if (playerState.Stamina < 15) { return; }

            playerState.StaminaUse(15);

            // �������̶�� ����
            if (GetComponent<Player_Attack>().IsAttack) { return; }                       

            IsDodge = true;
        }


        animator.SetBool("Dash", IsDodge);
        animator.SetFloat("MoveTime", moveTime);

        
    }

    // �뽬 ������ �̺�Ʈ
    public void ExitSlide()
    {
        IsDodge = false;
    }
}
