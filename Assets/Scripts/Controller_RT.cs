using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_RT : MonoBehaviour
{
   // Start is called before the first frame update
   public GameObject bar;
   public GameObject scrController;
   public CameraController mainCam;
   public Transform targetTransform;
   

   void Start()
   {
      mainCam = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraController>();
   }
    
   public void OnEnable()
   {
      bar.SetActive(true);
      scrController.SetActive(true);
      mainCam.enabled = true;
      mainCam.FollowTarget(targetTransform,5);
   }
}
