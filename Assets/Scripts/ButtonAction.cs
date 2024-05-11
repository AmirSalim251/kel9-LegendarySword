using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bar;
    public GameObject button;
    public GameObject scrController;
    public GameObject mainCamObj;
     public CameraController mainCam;
     public Transform targetTransform;
    
   public void onClick(){
    
    bar.SetActive(true);
    button.SetActive(false);
    scrController.SetActive(true);
    mainCamObj.SetActive(true);
    mainCam.FollowTarget(targetTransform,5);
   }
}
