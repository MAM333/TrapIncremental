using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] AudioClip mainMenu;
    [SerializeField] AudioClip game;
    [SerializeField] AudioClip bossFight;
    [SerializeField] AudioClip win;

    [SerializeField] float mainMenuV = 0.166f;
    [SerializeField] float gameV = 0.166f;
    [SerializeField] float bossFightV = 0.166f;
    [SerializeField] float winV = 0.166f;

    AudioSource audioSource;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();

        PlayMainMenu();
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip == audioSource.clip) return; 

        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void PlayMainMenu()
    {
        PlayClip(mainMenu, mainMenuV);
    }

    public void PlayGame()
    {
        PlayClip(game, gameV);
    }

    public void PlayBossFight()
    {
        PlayClip(bossFight, bossFightV);
    }

    public void PlayWin()
    {
        PlayClip(win, winV);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
