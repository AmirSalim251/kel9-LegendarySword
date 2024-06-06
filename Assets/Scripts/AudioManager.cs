using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("BGM")]
    public AudioClip mainMenuSong;
    public AudioClip stageSelectionSong;
    public AudioClip vnSong;
    public AudioClip combatSong;
    public AudioClip winSong;
    public AudioClip loseSong;

    [Header("SFX")]
    public Sound[] sfxSounds;

    [Header("Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    private string currentScene;

    [Header("Settings")]
    [SerializeField] Slider volumeSliderBGM;
    [SerializeField] Slider volumeSliderSFX;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            /*bgmSource = GetComponent<AudioSource>();*/
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        volumeSliderBGM = GameObject.FindGameObjectWithTag("BGM Volume Slider").GetComponent<Slider>();
        volumeSliderSFX = GameObject.FindGameObjectWithTag("SFX Volume Slider").GetComponent<Slider>();

        volumeSliderBGM.onValueChanged.AddListener(delegate { AudioManager.Instance.ChangeVolumeBGM(); });
        volumeSliderSFX.onValueChanged.AddListener(delegate { AudioManager.Instance.ChangeVolumeSFX(); });

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVolumeBGM();
        }
        else
        {
            LoadVolumeBGM();
        }

        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 1);
            LoadVolumeSFX();
        }
        else
        {
            LoadVolumeSFX();
        }

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
        else if (currentScene == "MainMenu" || currentScene == "Menu" || currentScene == "Credits")
        {
            PlayMusic(mainMenuSong);
        }
        else if (currentScene == "VN Scene")
        {
            PlayMusic(vnSong);
        }
        else if (currentScene == "CombatScene 3")
        {
            PlayMusic(combatSong);
        }

        if (currentScene == "MainMenu" || currentScene == "CombatScene 3")
        {
            volumeSliderBGM = GameObject.FindGameObjectWithTag("BGM Volume Slider").GetComponent<Slider>();
            volumeSliderSFX = GameObject.FindGameObjectWithTag("SFX Volume Slider").GetComponent<Slider>();
        }

        if (volumeSliderBGM != null && volumeSliderSFX != null)
        {
            volumeSliderBGM.onValueChanged.AddListener(delegate { AudioManager.Instance.ChangeVolumeBGM(); });
            volumeSliderSFX.onValueChanged.AddListener(delegate { AudioManager.Instance.ChangeVolumeSFX(); });

            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
                LoadVolumeBGM();
            }
            else
            {
                LoadVolumeBGM();
            }

            if (!PlayerPrefs.HasKey("sfxVolume"))
            {
                PlayerPrefs.SetFloat("sfxVolume", 1);
                LoadVolumeSFX();
            }
            else
            {
                LoadVolumeSFX();
            }
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (bgmSource.clip != clip)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }
    }

    //SFX Section
    public void PlaySFX(string path)
    {
        Sound s = Array.Find(sfxSounds, x=> x.name == path);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ChangeVolumeBGM()
    {
        bgmSource.volume = volumeSliderBGM.value;
        SaveVolumeBGM();
    }

    public void SaveVolumeBGM()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSliderBGM.value);
    }

    public void LoadVolumeBGM()
    {
        volumeSliderBGM.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void ChangeVolumeSFX()
    {
        sfxSource.volume = volumeSliderSFX.value;
        SaveVolumeSFX();
    }

    public void SaveVolumeSFX()
    {
        PlayerPrefs.SetFloat("sfxVolume", volumeSliderSFX.value);
    }

    public void LoadVolumeSFX()
    {
        volumeSliderSFX.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}

