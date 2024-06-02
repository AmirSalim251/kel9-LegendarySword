using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SpriteFaceCamera : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {   
        transform.rotation = Quaternion.Euler(0f, mainCamera.transform.rotation.eulerAngles.y, 0f);
    }
}
