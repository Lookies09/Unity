using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    //�ִϸ�����
    private Animator animator;

    // ���� �ð� ���
    private float aTime;

    // ���� ���� ���� �ð�
    private float aDelayTime;

    // ���� Ÿ��
    private int attackType = 0;

    // ���� Ȯ��
    private bool isAttack;

    // ��ų Ȯ��
    private bool onSkill;

    // ���� Ȯ��
    private bool isAiming;

    // ������ ���� Ȯ��
    private bool isDodgeAttack;

    // ���ؼ�
    [SerializeField] private GameObject aimLine;

    // �ѱ� ��ġ �迭 (�� = 0, �� = 1)
    [SerializeField] private Transform[] shootPoses;

    // �Ѿ� ������
    [SerializeField] private GameObject bulletPrefab;

    // ���� ����Ʈ
    [SerializeField] private GameObject shootEffect;

    // ��ų ���� ����Ʈ
    [SerializeField] private GameObject skillShootEffect;

    // ���� ���� ����Ʈ
    [SerializeField] private GameObject aimShootEffect;

    // ����ĳ��Ʈ �浹 ���̾�
    [SerializeField] private LayerMask layerMask;

    // �÷��̾� ���� ����
    private Player_Health playerState;

    // Ŭ�� ��ġ
    private Vector3 clickPos;

    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public bool OnSkill { get => onSkill; set => onSkill = value; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerState = GetComponent<Player_Health>();
    }

    // Update is called once per frame
    void Update()
    {       
        // �¾Ұų� �׾����� ����
        if (GetComponent<Player_Health>().IsHit || GetComponent<Player_Health>().IsDead)
        {
            onSkill = false;
            return;
        }

        DodgeAttack();

        AimAttack();

        Attack();

        SkillAttack();        

        animator.SetBool("OnAttack", IsAttack);
        animator.SetBool("Skill", OnSkill);
        animator.SetBool("IsAiming", isAiming);
        animator.SetBool("IsDodgeAttack", isDodgeAttack);
    }

    public void Attack()
    {
        //  ���� ���׹̳ʰ� ��� ���¹̳� ���� �۴ٸ� ����
        if (playerState.Stamina < 5) { return; }

        // ������ �����̳� ���������̶�� ����
        if (isDodgeAttack || GetComponent<Player_Movement>().IsDodge) { return; }

        // ���콺 ��Ŭ���� ������ �ð��� �帣��
        if (Input.GetMouseButton(0) && OnSkill == false)
        {
            IsAttack = true;

            aTime += Time.deltaTime;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 Ŭ�� ��ġ�� Ray�� ��ȯ            
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Ray�� �浹�ϴ� ��ü�� �ִ��� Ȯ��
            {
                // �ε巴�� ȸ��
                Vector3 direc = (hit.point - transform.position);
                Quaternion rT = Quaternion.LookRotation(direc, Vector3.up);
                rT.x = 0;
                rT.z = 0;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rT, 1500 * Time.deltaTime);

                clickPos = hit.point;
            }
        }

        // ���콺 ��Ŭ���� ��� ���� Ÿ���� ���ϰ� �ð��� 0���� ����(������, �׳� ����)
        if (Input.GetMouseButtonUp(0) && OnSkill == false && aTime >= 0.13f)
        {     
            // ������ �� �ð��� 0.5�� �̸��̶��
            if (aTime < 0.5f)
            {   
                animator.SetTrigger("Attack");

                // �⺻����
                animator.SetInteger("AttackType", attackType);

                // ���� Ÿ���� 2 �̻��̸�
                if (attackType >= 2)
                {
                    attackType = 0;
                }
                else
                {
                    // ���� ���� Ÿ��
                    attackType++;
                    aDelayTime = 0;
                }
            }

            // �ð� �ʱ�ȭ
            aTime = 0;            
        }

        // ���� �޹� Ÿ���� 0 �ʰ��̸�
        if (attackType > 0)
        {
            // �����̽ð� �帣��
            aDelayTime += Time.deltaTime;

            if (aDelayTime > 1f)
            {
                attackType = 0;
                aDelayTime = 0;
            }

        }

        animator.SetFloat("ATime", aTime);
        
    }

    // ��ų ����
    public void SkillAttack()
    {
        //  ���� ���׹̳ʰ� ��� ���¹̳� ���� �۴ٸ� ����
        if (playerState.Stamina < 20) { return; }

        // ������ �����̳� ���������̶�� ����
        if (isDodgeAttack || GetComponent<Player_Movement>().IsDodge) { return; }

        // ���� Shift ������ ��
        if (Input.GetKeyDown(KeyCode.LeftShift) && OnSkill == false && IsAttack == false)
        {            
            playerState.StaminaUse(20);

            OnSkill = true;
            IsAttack = true;
        }
        
    }

    // ���콺 ��Ŭ������ �����ϴ� ����
    public void AimAttack()
    {
        //  ���� ���׹̳ʰ� ��� ���¹̳� ���� �۴ٸ� ����
        if (playerState.Stamina < 10) { return; }

        // ������ �����̳� ���������̶�� ����
        if (isDodgeAttack || GetComponent<Player_Movement>().IsDodge) { return; }

        if (Input.GetMouseButton(1) && OnSkill == false) 
        {
            // ���� Ȯ��
            IsAttack = true;
            // ���� Ȯ��
            isAiming = true;

            aimLine.SetActive(true);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 Ŭ�� ��ġ�� Ray�� ��ȯ            
            RaycastHit hit;

            // Ŭ�� ��ġ�� ȸ��
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Ray�� �浹�ϴ� ��ü�� �ִ��� Ȯ��
            {
                // �ε巴�� ȸ��
                Vector3 direc = (hit.point - transform.position);
                Quaternion rT = Quaternion.LookRotation(direc, Vector3.up);
                rT.x = 0;
                rT.z = 0;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rT, 1500 * Time.deltaTime);

                clickPos = hit.point;
                clickPos.y = shootPoses[1].position.y;
            }            
        }

        if (Input.GetMouseButtonUp(1) && OnSkill == false)
        {
            aimLine.SetActive(false);
            isAiming = false;
            animator.SetBool("IsAimAttack", true);
        }        
    }

    // ���� �� ������ Ǫ�� �̺�Ʈ
    public void AttackEnd()
    {
        IsAttack = false;
    }

    // ��ų ���� �� ������ Ǫ�� �̺�Ʈ
    public void SkillEnd()
    {
        OnSkill = false;
        AttackEnd();
    }

    
    // ���� �̺�Ʈ
    public void Shoot(int num)
    {
        playerState.StaminaUse(5);

        clickPos.y = shootPoses[num].position.y;

        // �Ѿ� ����
        Instantiate(bulletPrefab, shootPoses[num].position, Quaternion.LookRotation(clickPos - shootPoses[num].position));

        // ���� ����Ʈ ����
        Instantiate(shootEffect, shootPoses[num].position, Quaternion.LookRotation(clickPos - shootPoses[num].position));
    }

    // ���� ���� �̺�Ʈ
    public void AimShoot()
    {
        // ���� ����Ʈ ����
        Instantiate(aimShootEffect, shootPoses[0].position, Quaternion.LookRotation(clickPos - shootPoses[0].position));
    }

    // ������ ����
    public void DodgeAttack()
    {
        // ������ ���̰�
        if (GetComponent<Player_Movement>().IsDodge)
        {
            // ���콺 ��Ŭ���ϸ�
            if (Input.GetMouseButton(0))
            {
                // ���� �ð� �帣��
                aTime += Time.deltaTime;

                // ���� �ð��� 0.35�� �̻��̶��
                if (aTime >= 0.35f)
                {
                    IsAttack = true;
                    isDodgeAttack = true;

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ���콺 Ŭ�� ��ġ�� Ray�� ��ȯ            
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Ray�� �浹�ϴ� ��ü�� �ִ��� Ȯ��
                    {
                        // �ε巴�� ȸ��
                        Vector3 direc = (hit.point - transform.position);
                        Quaternion rT = Quaternion.LookRotation(direc, Vector3.up);
                        rT.x = 0;
                        rT.z = 0;

                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rT, 360);

                        clickPos = hit.point;
                        clickPos.y = shootPoses[1].position.y;
                    }

                    GetComponent<Player_Movement>().IsDodge = false;
                    aTime = 0;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                aTime = 0;
            }
        }
        
    }

    // ��ų ���� �̺�Ʈ
    public void SkillShoot()
    {
        StartCoroutine(SkillShootCorutine());

    }


    // �ѱ� �������� ��� �߻��ϴ� �ڷ�ƾ
    IEnumerator SkillShootCorutine()
    {
        while (true)
        {
            Quaternion rota = shootPoses[0].rotation;
            Quaternion rota1 = shootPoses[1].rotation;

            // �Ѿ� ����
            Instantiate(bulletPrefab, shootPoses[0].position, rota);
            Instantiate(bulletPrefab, shootPoses[1].position, rota1);

            // ���� ����Ʈ ����
            Instantiate(skillShootEffect, shootPoses[0].position, rota);
            Instantiate(skillShootEffect, shootPoses[1].position, rota1);

            yield return new WaitForSeconds(0.01f);
        }

    }

    // ���� ���� ��
    public void AimAttackEnd()
    {
        animator.SetBool("IsAimAttack", false);
        AttackEnd();
        
    }

    public void DodgeAttackEnd()
    {
        isDodgeAttack = false;
        IsAttack = false;
        OnSkill = false;
    }

    public void StopSkillShoot()
    {
        StopAllCoroutines();
    }

}
