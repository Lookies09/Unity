using System.Collections;
using UnityEngine;

public class ParabolicMovement : MonoBehaviour
{
    public Transform target;  // 목표 위치
    public float duration = 2.0f;  // 이동 시간
    public float height = 5.0f;  // 포물선의 정점 높이

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime = 0f;

    public void Jump()
    {
        StartCoroutine(MoveInParabola());
    }

    public void StopJump()
    {
        StopCoroutine(MoveInParabola());
    }

    IEnumerator MoveInParabola()
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 현재 시간 t에 따른 위치 계산
            float currentHeight = Mathf.Sin(Mathf.PI * t) * height; // 포물선의 높이
            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t); // 직선 이동
            currentPosition.y += currentHeight; // 높이 추가

            transform.position = currentPosition;

            yield return null;
        }

        // 이동이 끝난 후 정확히 목표 위치로 설정
        transform.position = targetPosition;
    }
}
