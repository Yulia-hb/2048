using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip _mergeClip;
    [SerializeField] private AudioClip _buttonClip;
    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private AudioClip _countdownClip;
    [SerializeField] private AudioClip _rewardClip;

    [Header("Music")]
    [SerializeField] private AudioClip _backgroundMusic;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (_backgroundMusic != null)
        {
            _musicSource.clip = _backgroundMusic;
            _musicSource.loop = true;
            _musicSource.Play();
        }
    }

    public void SetMusic(bool enabled)
    {
        _musicSource.mute = !enabled;
    }

    public void SetSfx(bool enabled)
    {
        _sfxSource.mute = !enabled;
    }
    public void PlayMerge()
    {
        PlaySFX(_mergeClip);
    }

    public void PlayButton()
    {
        PlaySFX(_buttonClip);
    }

    public void PlayGameOver()
    {
        PlaySFX(_gameOverClip);
    }

    public void PlayCountdown()
    {
        PlaySFX(_countdownClip);
    }

    public void PlayReward()
    {
        PlaySFX(_rewardClip);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;

        _sfxSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PlayMusic()
    {
        if (!_musicSource.isPlaying)
            _musicSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        _sfxSource.volume = Mathf.Clamp01(volume);
    }
}
