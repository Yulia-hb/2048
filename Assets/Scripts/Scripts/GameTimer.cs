using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    [Header("Timer")]
    [SerializeField] private float _startTime = 45f;
    [SerializeField] private float _continueTime = 30f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _finalScoreText;

    [Header("Managers")]
    [SerializeField] private ScoreManager _scoreManager;

    private float _currentTime;
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
        _isRunning = true;

        if (_gameOverPanel != null)
            _gameOverPanel.SetActive(false);

        UpdateTimerUI();
    }

    private void Update()
    {
        if (!_isRunning)
            return;

        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0)
        {
            _currentTime = 0;
            UpdateTimerUI();
            GameOver();
            return;
        }

        UpdateTimerUI();
    }

    public void AddTime(float seconds)
    {
        if (!_isRunning)
            return;

        _currentTime += seconds;
        UpdateTimerUI();
    }

    public void AddMergeTime(long cubeValue)
    {
        float timeToAdd = cubeValue switch
        {
            4 => 0.5f,
            8 => 1f,
            16 => 1.5f,
            32 => 2f,
            64 => 3f,
            128 => 4f,
            256 => 5f,
            _ => 0f
        };

        AddTime(timeToAdd);
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

    private void GameOver()
    {
        _isRunning = false;

        if (_finalScoreText != null && _scoreManager != null)
            _finalScoreText.text = $"Your Score\n{_scoreManager.Score}";

        if (_gameOverPanel != null)
            _gameOverPanel.SetActive(true);

        if (_timerPulseTween != null)
        {
            _timerPulseTween.Kill();
            _timerPulseTween = null;
        }

        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        _currentTime = _continueTime;

        if (_gameOverPanel != null)
            _gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
        _isRunning = true;

        UpdateTimerUI();
    }
}