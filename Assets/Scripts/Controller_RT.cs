using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Controller_RT : MonoBehaviour
{
   public GameObject bar;
   public GameObject scrController;
   public Transform targetTransform;
   public RagdollController rController;

   public void OnEnable()
   {
      bar.SetActive(true);
      scrController.SetActive(true);
      GameObject.Find("AlexCam").GetComponent<CinemachineVirtualCamera>().enabled = true;
      rController.RagdollOn();


   }
}
