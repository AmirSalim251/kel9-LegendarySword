using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectFaceCamera : MonoBehaviour
{
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
        transform.forward = cam.transform.forward;
    }
}
