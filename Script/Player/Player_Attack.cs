using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    //애니메이터
    private Animator animator;

    // 공격 시간 계산
    private float aTime;

    // 공격 지연 가능 시간
    private float aDelayTime;

    // 공격 타입
    private int attackType = 0;

    // 공격 확인
    private bool isAttack;

    // 스킬 확인
    private bool onSkill;

    // 조준 확인
    private bool isAiming;

    // 구르기 공격 확인
    private bool isDodgeAttack;

    // 조준선
    [SerializeField] private GameObject aimLine;

    // 총구 위치 배열 (좌 = 0, 우 = 1)
    [SerializeField] private Transform[] shootPoses;

    // 총알 프리펩
    [SerializeField] private GameObject bulletPrefab;

    // 발포 이펙트
    [SerializeField] private GameObject shootEffect;

    // 스킬 발포 이펙트
    [SerializeField] private GameObject skillShootEffect;

    // 조준 발포 이펙트
    [SerializeField] private GameObject aimShootEffect;

    // 레이캐스트 충돌 레이어
    [SerializeField] private LayerMask layerMask;

    // 플레이어 상태 참조
    private Player_Health playerState;

    // 클릭 위치
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
        // 맞았거나 죽었으면 리턴
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
        //  현재 스테미너가 사용 스태미너 보다 작다면 리턴
        if (playerState.Stamina < 5) { return; }

        // 구르기 공격이나 구르기중이라면 리턴
        if (isDodgeAttack || GetComponent<Player_Movement>().IsDodge) { return; }

        // 마우스 좌클릭을 누르면 시간이 흐르고
        if (Input.GetMouseButton(0) && OnSkill == false)
        {
            IsAttack = true;

            aTime += Time.deltaTime;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 클릭 위치를 Ray로 변환            
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Ray와 충돌하는 물체가 있는지 확인
            {
                // 부드럽게 회전
                Vector3 direc = (hit.point - transform.position);
                Quaternion rT = Quaternion.LookRotation(direc, Vector3.up);
                rT.x = 0;
                rT.z = 0;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rT, 1500 * Time.deltaTime);

                clickPos = hit.point;
            }
        }

        // 마우스 좌클릭을 뗴면 공격 타입을 정하고 시간을 0으로 만듬(강공격, 그냥 공격)
        if (Input.GetMouseButtonUp(0) && OnSkill == false && aTime >= 0.13f)
        {     
            // 눌렀다 땐 시간이 0.5초 미만이라면
            if (aTime < 0.5f)
            {   
                animator.SetTrigger("Attack");

                // 기본공격
                animator.SetInteger("AttackType", attackType);

                // 공격 타입이 2 이상이면
                if (attackType >= 2)
                {
                    attackType = 0;
                }
                else
                {
                    // 다음 공격 타입
                    attackType++;
                    aDelayTime = 0;
                }
            }

            // 시간 초기화
            aTime = 0;            
        }

        // 공격 콤버 타입이 0 초과이면
        if (attackType > 0)
        {
            // 딜레이시간 흐르기
            aDelayTime += Time.deltaTime;

            if (aDelayTime > 1f)
            {
                attackType = 0;
                aDelayTime = 0;
            }

        }

        animator.SetFloat("ATime", aTime);
        
    }

    // 스킬 공격
    public void SkillAttack()
    {
        //  현재 스테미너가 사용 스태미너 보다 작다면 리턴
        if (playerState.Stamina < 20) { return; }

        // 구르기 공격이나 구르기중이라면 리턴
        if (isDodgeAttack || GetComponent<Player_Movement>().IsDodge) { return; }

        // 왼쪽 Shift 눌렀을 시
        if (Input.GetKeyDown(KeyCode.LeftShift) && OnSkill == false && IsAttack == false)
        {            
            playerState.StaminaUse(20);

            OnSkill = true;
            IsAttack = true;
        }
        
    }

    // 마우스 우클릭으로 저격하는 공격
    public void AimAttack()
    {
        //  현재 스테미너가 사용 스태미너 보다 작다면 리턴
        if (playerState.Stamina < 10) { return; }

        // 구르기 공격이나 구르기중이라면 리턴
        if (isDodgeAttack || GetComponent<Player_Movement>().IsDodge) { return; }

        if (Input.GetMouseButton(1) && OnSkill == false) 
        {
            // 공격 확인
            IsAttack = true;
            // 조준 확인
            isAiming = true;

            aimLine.SetActive(true);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 클릭 위치를 Ray로 변환            
            RaycastHit hit;

            // 클릭 위치로 회전
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Ray와 충돌하는 물체가 있는지 확인
            {
                // 부드럽게 회전
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

    // 공격 후 움직임 푸는 이벤트
    public void AttackEnd()
    {
        IsAttack = false;
    }

    // 스킬 공격 후 움직임 푸는 이벤트
    public void SkillEnd()
    {
        OnSkill = false;
        AttackEnd();
    }

    
    // 발포 이벤트
    public void Shoot(int num)
    {
        playerState.StaminaUse(5);

        clickPos.y = shootPoses[num].position.y;

        // 총알 생성
        Instantiate(bulletPrefab, shootPoses[num].position, Quaternion.LookRotation(clickPos - shootPoses[num].position));

        // 발포 이펙트 생성
        Instantiate(shootEffect, shootPoses[num].position, Quaternion.LookRotation(clickPos - shootPoses[num].position));
    }

    // 조준 발포 이벤트
    public void AimShoot()
    {
        // 발포 이펙트 생성
        Instantiate(aimShootEffect, shootPoses[0].position, Quaternion.LookRotation(clickPos - shootPoses[0].position));
    }

    // 구르기 공격
    public void DodgeAttack()
    {
        // 구르는 중이고
        if (GetComponent<Player_Movement>().IsDodge)
        {
            // 마우스 좌클릭하면
            if (Input.GetMouseButton(0))
            {
                // 공격 시간 흐르고
                aTime += Time.deltaTime;

                // 누른 시간이 0.35초 이상이라면
                if (aTime >= 0.35f)
                {
                    IsAttack = true;
                    isDodgeAttack = true;

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 마우스 클릭 위치를 Ray로 변환            
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) // Ray와 충돌하는 물체가 있는지 확인
                    {
                        // 부드럽게 회전
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

    // 스킬 발포 이벤트
    public void SkillShoot()
    {
        StartCoroutine(SkillShootCorutine());

    }


    // 총구 방향으로 계속 발사하는 코루틴
    IEnumerator SkillShootCorutine()
    {
        while (true)
        {
            Quaternion rota = shootPoses[0].rotation;
            Quaternion rota1 = shootPoses[1].rotation;

            // 총알 생성
            Instantiate(bulletPrefab, shootPoses[0].position, rota);
            Instantiate(bulletPrefab, shootPoses[1].position, rota1);

            // 발포 이펙트 생성
            Instantiate(skillShootEffect, shootPoses[0].position, rota);
            Instantiate(skillShootEffect, shootPoses[1].position, rota1);

            yield return new WaitForSeconds(0.01f);
        }

    }

    // 조준 공격 끝
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
