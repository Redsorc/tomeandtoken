using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    public AudioClip menuLoop;
    [Header("SFX")]
    public AudioClip hoverSfx, clickSfx, panelSfx;

    AudioSource musicSource, sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Two AudioSources: one for music, one for SFX
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = menuLoop;
            musicSource.loop = true;
            musicSource.volume = 0.5f;
            musicSource.playOnAwake = false;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;

            musicSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayHover()
    {
        sfxSource.PlayOneShot(hoverSfx, 0.7f);
    }

    public void PlayClick()
    {
        sfxSource.PlayOneShot(clickSfx, 1f);
    }

    public void PlayPanel()
    {
        sfxSource.PlayOneShot(panelSfx, 0.8f);
    }
}
