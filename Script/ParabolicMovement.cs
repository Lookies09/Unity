using System.Collections;
using UnityEngine;

public class ParabolicMovement : MonoBehaviour
{
    public Transform target;  // ��ǥ ��ġ
    public float duration = 2.0f;  // �̵� �ð�
    public float height = 5.0f;  // �������� ���� ����

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

            // ���� �ð� t�� ���� ��ġ ���
            float currentHeight = Mathf.Sin(Mathf.PI * t) * height; // �������� ����
            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, t); // ���� �̵�
            currentPosition.y += currentHeight; // ���� �߰�

            transform.position = currentPosition;

            yield return null;
        }

        // �̵��� ���� �� ��Ȯ�� ��ǥ ��ġ�� ����
        transform.position = targetPosition;
    }
}
