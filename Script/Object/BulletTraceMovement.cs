using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알 추격 움직임 
public class BulletTraceMovement : MonoBehaviour
{
    // 추격시간
    [SerializeField] private float traceTime;

    // 총알 속도
    [SerializeField] private float speed;

    // 계산 시간
    private float time;

    // 추격할 대상
    private GameObject target;

    // 추격할 대상의 테그
    [SerializeField] private string targetTag;

    // 타겟 위치
    private Vector3 targetPos;

    // 방향
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag(targetTag);
        
    }

    // Update is called once per frame
    void Update()
    {
        // 시간 흐름
        time += Time.deltaTime;

        targetPos = target.transform.position;

        // 타겟이 발 밑으로 설정되어있어서 위로 올려줌
        targetPos.y += 1.6f;
                

        // traceTime 만큼 추격하고
        if (time < traceTime)
        {
            // 날아갈 방향 정해줌
            direction = targetPos - transform.position;
            direction = direction.normalized;

            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * (speed + 6) * Time.deltaTime, Space.World);
        }
        

    }
}
