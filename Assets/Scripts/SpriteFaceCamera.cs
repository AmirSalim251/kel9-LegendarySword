using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SpriteFaceCamera : MonoBehaviour
{
    public Camera mainCamera;

    void LateUpdate()
    {
        Vector3 CameraPosition = mainCamera.transform.position;
        CameraPosition.y = transform.position.y;
        transform.LookAt(CameraPosition);
        transform.Rotate(0f, 180f, 0f);
    }
}
