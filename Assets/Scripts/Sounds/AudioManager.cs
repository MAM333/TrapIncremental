using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("AudioSources")]
    int numOfAudioSources = 5;
    [SerializeField] GameObject soundPrefab;

    [Header("Sounds")]
    [SerializeField] AudioClip spikeTrap;
    [SerializeField] AudioClip explosiveTrap;
    [SerializeField] AudioClip thunderTrap;
    [SerializeField] AudioClip enemyDied;
    [SerializeField] AudioClip upgrade;
    [SerializeField] AudioClip notUpgrade;
    [SerializeField] AudioClip trapPlace;
    [SerializeField] AudioClip dead;
    [SerializeField] AudioClip nextRound;
    [SerializeField] float spikeTrapV = 0.166f;
    [SerializeField] float explosiveTrapV = 0.166f;
    [SerializeField] float thunderTrapV = 0.166f;
    [SerializeField] float enemyDiedV = 0.166f;
    [SerializeField] float upgradeV = 0.166f;
    [SerializeField] float notUpgradeV = 0.166f;
    [SerializeField] float trapPlaceV = 0.166f;
    [SerializeField] float deadV = 0.166f;
    [SerializeField] float nextRoundV = 0.166f;

    List<AudioSource> soundSources = new();
    void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        else instance = this;

        soundSources = new List<AudioSource>();
        for (int i = 0; i < numOfAudioSources; i++)
        {
            GameObject soundPf = Instantiate(soundPrefab, transform);
            AudioSource audioS = soundPf.GetComponent<AudioSource>();
            soundSources.Add(audioS);
        }

        DontDestroyOnLoad(gameObject);
    }

    int nextSoundIndex = 0;
    private void PlaySound(AudioClip clip, float volume)
    {
        AudioSource source = soundSources[nextSoundIndex];
        source.Stop();
        source.volume = volume;
        source.clip = clip;
        source.Play();
        ++nextSoundIndex;
        if (nextSoundIndex >= soundSources.Count) nextSoundIndex = 0;
    }
    
    public void PlaySpikeTrap()
    {
        PlaySound(spikeTrap, spikeTrapV);
    }

    public void PlayExplosiveTrap()
    {
        PlaySound(explosiveTrap, explosiveTrapV);
    }

    public void PlayThunderTrap()
    {
        PlaySound(thunderTrap, thunderTrapV);
    }

    bool canPlayEnemyDied = true;
    public void PlayEnemyDied()
    {
        if (canPlayEnemyDied) 
        { 
            PlaySound(enemyDied, enemyDiedV);
            StartCoroutine(DoNotPlayEnemyDiedSound());
        }
    }

    IEnumerator DoNotPlayEnemyDiedSound()
    {
        canPlayEnemyDied = false;

        yield return new WaitForSeconds(0.3f);

        canPlayEnemyDied = true;
    }

    public void PlayUpgrade()
    {
        PlaySound(upgrade, upgradeV);
    }

    public void PlayNotUpgrade()
    {
        PlaySound(notUpgrade, notUpgradeV);
    }

    public void PlayTrapPlace()
    {
        PlaySound(trapPlace, trapPlaceV);
    }

    public void PlayDeadSound()
    {
        PlaySound(dead, deadV);
    }

    public void PlayNextRound()
    {
        PlaySound(nextRound, nextRoundV);
    }
}
