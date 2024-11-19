using UnityEngine;

public class EnemyAttackAbleState : EnemyState
{
    [SerializeField] protected float attackDistance; // 플레이어 공격 가능 거리
    [SerializeField] protected float detectDistance; // 플레이어 추적 가능 거리
    [SerializeField] protected float slerpValue = 20;

    public override void EnterState(int state)
    {
    }

    public override void ExitState()
    {
    }

    public override void UpdateState()
    {
    }

    // 공격 대상을 주시
    public void LookAtTarget(bool isOn)
    {
        if (isOn)
        {
            // 공격 대상을 향한 방향을 계산
            Vector3 direction = (GameObject.Find("Player").transform.position - transform.position).normalized;

            // 회전 쿼터니언 계산
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

            // 보간 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, slerpValue * Time.deltaTime);
        }
    }
}
