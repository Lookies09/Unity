using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // ȭ�� �ӵ�
    [SerializeField] protected float speed;    


    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
}
