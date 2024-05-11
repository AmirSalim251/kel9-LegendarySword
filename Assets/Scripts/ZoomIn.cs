using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
   public Collider objek;
  public CameraController cameraController;
  public float length;

  private void OnTriggerEnter(Collider other)
  {
    if (other == objek)
    {
        cameraController.FollowTarget(objek.transform, length);
    }
  }
}
