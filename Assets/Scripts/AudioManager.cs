using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip mainMenuSong;
    public AudioClip stageSelectionSong;

    private AudioSource audioSource;

    private string currentScene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayMusic(mainMenuSong); // Start with the main menu song
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;

        if (currentScene == "StageSelection")
        {
            PlayMusic(stageSelectionSong);
        }
        else if (currentScene == "MainMenu" || currentScene == "Menu")
        {
            PlayMusic(mainMenuSong);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
