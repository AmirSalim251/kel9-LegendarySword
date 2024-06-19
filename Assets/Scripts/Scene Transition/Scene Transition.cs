using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    public Animator animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            animator = GetComponent<Animator>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }
    
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        StartScene(currentScene);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(EndScene(sceneName));
    }

    void StartScene(string currentScene)
    {
        string lastScene = SceneTracker.lastScene;

        if (lastScene == "MainMenu" || lastScene == "StageSelection" || lastScene == "Credits")
            animator.Play("Fade In");

        else if (lastScene == "VN Scene" || lastScene == "CombatScene 3")
            animator.Play("Circle Wipe In");
    }

    IEnumerator EndScene(string nextScene)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MainMenu" || currentScene == "StageSelection" || currentScene == "Credits")
            animator.Play("Fade Out");

        else if (currentScene == "VN Scene" || currentScene == "CombatScene 3")
            animator.Play("Circle Wipe Out");

        yield return new WaitForSeconds(1);
        
        SceneTracker.lastScene = currentScene;
        SceneManager.LoadScene(nextScene);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentScene = scene.name;
        StartScene(currentScene);
    }
}
