using UnityEngine;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuScreen : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _vibrationButton;

    [Header("Music")]
    [SerializeField] private Sprite _musicOnSprite;
    [SerializeField] private Sprite _musicOffSprite;
    [SerializeField] private TMP_Text _musicText;

    [Header("Sound")]
    [SerializeField] private Sprite _soundOnSprite;
    [SerializeField] private Sprite _soundOffSprite;
    [SerializeField] private TMP_Text _soundText;

    [Header("Vibration")]
    [SerializeField] private Sprite _vibrationOnSprite;
    [SerializeField] private Sprite _vibrationOffSprite;
    [SerializeField] private TMP_Text _vibrationText;

    [SerializeField] private SoundManager _soundManager;

    private bool _musicEnabled = true;
    private bool _soundEnabled = true;
    private bool _vibrationEnabled = true;

    private Image _musicImage;
    private Image _soundImage;
    private Image _vibrationImage;

    private void Awake()
    {
        _musicImage = _musicButton.GetComponent<Image>();
        _soundImage = _soundButton.GetComponent<Image>();
        _vibrationImage = _vibrationButton.GetComponent<Image>();

        _closeButton.onClick.AddListener(Close);

        _musicButton.onClick.AddListener(ToggleMusic);
        _soundButton.onClick.AddListener(ToggleSound);
        _vibrationButton.onClick.AddListener(ToggleVibration);

        RefreshUI();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    private void ToggleMusic()
    {
        _musicEnabled = !_musicEnabled;

        _soundManager.SetMusic(_musicEnabled);

        RefreshUI();
    }

    private void ToggleSound()
    {
        _soundEnabled = !_soundEnabled;

        _soundManager.SetSfx(_soundEnabled);

        RefreshUI();
    }

    private void ToggleVibration()
    {
        _vibrationEnabled = !_vibrationEnabled;

        RefreshUI();
    }

    private void RefreshUI()
    {
        _musicImage.sprite = _musicEnabled ? _musicOnSprite : _musicOffSprite;
        _soundImage.sprite = _soundEnabled ? _soundOnSprite : _soundOffSprite;
        _vibrationImage.sprite = _vibrationEnabled ? _vibrationOnSprite : _vibrationOffSprite;

       
    }
}