using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject backButton;

    void Awake()
    {
        maxPage = levelPagesRect.transform.childCount;
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && currentPage != maxPage)
        {
            Next();
            StartCoroutine(SimulateButtonPress(nextButton.GetComponent<Button>()));
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentPage != 1)
        {
            Previous();
            StartCoroutine(SimulateButtonPress(prevButton.GetComponent<Button>()));
        }

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
            StartCoroutine(SimulateButtonPress(backButton.GetComponent<Button>()));
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

    IEnumerator SimulateButtonPress(Button button)
    {
        ColorBlock colors = button.colors;

        // Change to pressed color
        button.image.CrossFadeColor(colors.pressedColor, colors.fadeDuration, true, true);
        yield return new WaitForSeconds(colors.fadeDuration);

        // Change back to normal color
        button.image.CrossFadeColor(colors.normalColor, colors.fadeDuration, true, true);
    }
}


