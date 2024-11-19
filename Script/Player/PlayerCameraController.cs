using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private GameObject cinemaCamera;

    private int rotate = 0;


    void Update()
    {
        Quaternion startRot = cinemaCamera.transform.rotation;

        Quaternion endRot = Quaternion.Euler(40, rotate, 0);

        Quaternion result = Quaternion.Lerp(startRot, endRot, 5 * Time.deltaTime);

        cinemaCamera.transform.rotation = result;

        if (Input.GetKeyDown(KeyCode.E))
        {
            rotate += 45;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotate -= 45;
        }
    }

}
