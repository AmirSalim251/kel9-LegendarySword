using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Bar : MonoBehaviour
{
    public Image bar;
    public GameObject bg;
    public GameObject scrController;
    public GameObject cube;
    public GameObject mainCamObj;
    public CameraController mainCam;
    Controller_Battle ControllerBattle;
    Controller_RT rtController;

    public float timeLimit = 5f; // Total time to decrease the bar to zero
    public float fillAmount = 1f; // Initial fill amount (1.0 = 100%, 0.5 = 50%)

    private Vector3 cubeInitialPosition;
    private Quaternion cubeInitialRotation;
    private Rigidbody cubeRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rtController = GameObject.FindGameObjectWithTag("RTController").GetComponent<Controller_RT>();
        ControllerBattle = GameObject.FindGameObjectWithTag("BattleController").GetComponent<Controller_Battle>();

        bar.fillAmount = fillAmount;
        
        cubeInitialPosition = cube.transform.position;
        cubeInitialRotation = cube.transform.rotation;
        cubeRigidbody = cube.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fillAmount > 0)
        {
            fillAmount -= Time.deltaTime / timeLimit;
            bar.fillAmount = fillAmount;

            if (fillAmount <= 0)
            {
                StartCoroutine(OnBarDepleted());
            }
        }
    }

    IEnumerator OnBarDepleted()
    {
        // mainCam.GoBackToDefault(); 
        GameObject.Find("RTCam").GetComponent<CinemachineVirtualCamera>().enabled = false;

        // Deactivate character controller
        scrController.SetActive(false);

        yield return new WaitForSeconds(2);

        bg.SetActive(false);
        rtController.enabled = false;
        
        // Reset cube position and rotation
        ResetCubePosition();
        ControllerBattle.PassTurn();

        // Reset fillAmount for next use if needed
        fillAmount = 1f;
        bar.fillAmount = fillAmount;
    }

    void ResetCubePosition()
    {
        cube.transform.position = cubeInitialPosition;
        cube.transform.rotation = cubeInitialRotation;
    }
}
