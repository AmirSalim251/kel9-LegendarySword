using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float returnTime;       // Time needed to return to the default position
    public float followSpeed;      // Speed at which the camera follows the target
    public float length;           // Distance from the target

    public Transform target;       // The target to follow
    public Transform mainCam;      // The camera's transform
    private Vector3 defaultPosition; // The default position of the camera

    public bool hasTarget { get { return target != null; } }

    // private void Start()
    // {
    //     defaultPosition = mainCam.transform.position;
    // }

    // private void Update()
    // {
    //     if (hasTarget)
    //     {
    //         Vector3 targetToCameraDirection = mainCam.transform.rotation * -Vector3.forward;
    //         Vector3 targetPosition = target.position + (targetToCameraDirection * length);

    //         // Maintain the current Y position of the camera
    //         targetPosition.y = mainCam.transform.position.y;

    //         // Adjust the X position to be closer to the character
    //         targetPosition.x = Mathf.Lerp(mainCam.transform.position.x, target.position.x, 1f);

    //         mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, targetPosition, followSpeed * Time.deltaTime);
    //     }
    // }

    // public void FollowTarget(Transform targetTransform, float targetLength)
    // {
    //     StopAllCoroutines();
    //     target = targetTransform;
    //     length = targetLength;
    // }

    // public void GoBackToDefault()
    // {
    //     StopAllCoroutines();
    //     target = null;
    //     StartCoroutine(MovePosition(defaultPosition, returnTime));
    // }

    // private IEnumerator MovePosition(Vector3 target, float time)
    // {
    //     float timer = 0;
    //     Vector3 start = mainCam.transform.position;

    //     while (timer < time)
    //     {
    //         mainCam.transform.position = Vector3.Lerp(start, target, Mathf.SmoothStep(0.0f, 1.0f, timer / time));
    //         timer += Time.deltaTime;
    //         yield return null;
    //     }
    //     mainCam.transform.position = target;
    // }
    
}
