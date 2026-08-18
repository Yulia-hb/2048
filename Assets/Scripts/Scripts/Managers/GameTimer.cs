using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    [Header("Timer")]
    [SerializeField] private float _startTime = 45f;
    [SerializeField] private float _bonusTime = 30f;
    [SerializeField] private float _rewardAdTime = 60f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _timerText;

    [Header("References")]
    [SerializeField] private GameOverPanelView _gameOverPanelView;

    private float _currentTime;
    private float _maxTime;
    private bool _isRunning;

    private Tween _timerPulseTween;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _currentTime = _startTime;
        _maxTime = _startTime;
        _isRunning = true;

        UpdateTimerUI();
    }

    private void Update()
    {
        if (!_isRunning)
            return;

        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0f)
        {
            _currentTime = 0f;
            UpdateTimerUI();
            GameOver();
            return;
        }

        UpdateTimerUI();
    }

    /// <summary>
    /// Додає звичайний час (Merge).
    /// </summary>
    public void AddTime(float seconds)
    {
        if (!_isRunning)
            return;

        _currentTime += seconds;
        _currentTime = Mathf.Min(_currentTime, _maxTime);

        UpdateTimerUI();
    }

    /// <summary>
    /// Додає час залежно від значення кубика.
    /// </summary>
    public float AddMergeTime(long cubeValue)
    {
        float timeToAdd = cubeValue switch
        {
            4 => 0.5f,
            8 => 0.5f,
            16 => 1f,
            32 => 1f,
            64 => 1.5f,
            128 => 1.5f,
            256 => 2f,
            512 => 2.5f,
            1024 => 3f,
            2048 => 3.5f,

            _ => 0f
        };

        AddTime(timeToAdd);

        return timeToAdd;
    }

    /// <summary>
    /// Кнопка +30 секунд.
    /// </summary>
    public void AddThirtySeconds()
    {
        _maxTime = _bonusTime;
        _currentTime = _bonusTime;

        _isRunning = true;

        _gameOverPanelView?.Hide();

        Time.timeScale = 1f;

        UpdateTimerUI();
    }

    /// <summary>
    /// Продовжити після реклами (+60 секунд).
    /// </summary>
    public void ContinueAfterAd()
    {
        _maxTime = _rewardAdTime;
        _currentTime = _rewardAdTime;

        _isRunning = true;

        _gameOverPanelView?.Hide();

        Time.timeScale = 1f;

        UpdateTimerUI();
    }

    private void GameOver()

    {
        Debug.Log("GameOver");
        _isRunning = false;

        if (_timerPulseTween != null)
        {
            _timerPulseTween.Kill();
            _timerPulseTween = null;
        }

        Time.timeScale = 0f;

        _gameOverPanelView?.Show();
    }

    private void UpdateTimerUI()
    {
        if (_timerText == null)
            return;

        TimeSpan time = TimeSpan.FromSeconds(_currentTime);

        _timerText.text = $"{time.Minutes:00}:{time.Seconds:00}";

        if (_currentTime <= 10f)
        {
            _timerText.color = Color.red;

            if (_timerPulseTween == null || !_timerPulseTween.IsActive())
            {
                _timerPulseTween = _timerText.transform
                    .DOScale(1.5f, 0.35f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }
        }
        else
        {
            _timerText.color = Color.greenYellow;

            if (_timerPulseTween != null)
            {
                _timerPulseTween.Kill();
                _timerPulseTween = null;
            }

            _timerText.transform.localScale = Vector3.one;
        }
    }
}