using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class Bar : MonoBehaviour
{
    public GameObject bar;
    public GameObject button;
    public GameObject bg;
    private RectTransform bar_scale;
    public GameObject scrController;
    public GameObject cube;
    public GameObject mainCamObj;
    public CameraController mainCam;

    
   

    public int time;
    // Start is called before the first frame update
    void Start()
    {
        
        bar_scale = bar.GetComponent<RectTransform>();
        
        
    }

    // Update is called once per frame
    void Update()
    { 
        if(bar_scale.localScale.x == 286){
            animateBar();
        }
             
    }

    public void animateBar(){
       
        LeanTween.scaleX(bar,-1,time).setOnComplete(activateGameObj);
        
    }

    public void activateGameObj(){
        button.SetActive(true);
        bar_scale.localScale = new Vector3(286,1,1);
        bg.SetActive(false);
        scrController.SetActive(false);
        mainCam.GoBackToDefault();
        //mainCamObj.SetActive(false);
        
        setDefaultPos();
     
    }

    public void setDefaultPos(){
        cube.GetComponent<Transform>().position = new Vector3(0,1f,0);

    }


}
