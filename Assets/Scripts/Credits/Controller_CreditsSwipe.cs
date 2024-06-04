using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_CreditsSwipe : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;

    LTDescr tween;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    [Header("Buttons")]
    public GameObject prevButton;
    public GameObject nextButton;

    void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
    }

    void Update()
    {
        if (currentPage == 1)
        {
            prevButton.SetActive(false);
        }
        else if (currentPage == maxPage)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
            prevButton.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            BackToMenu();
        }
    }

    public void Next()
    {
        if(currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();

        }
    }

    void MovePage()
    {
        if (tween != null)
            tween.reset();
        tween = levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
