using UnityEngine;
using UnityEngine.SceneManagement;
using ChainCube.Scripts.Records;
using ChainCube.Scripts.Rewards;

public class GameFlowManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private RecordManager _recordManager;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private ResultsPanelView _resultsPanelView;

    [Header("HUD")]
    [SerializeField] private GameObject _gameplayHUD;
    [SerializeField] private GameObject _globalHUD;

    private bool _runFinished;

    private void Start()
    {
        if (_gameplayHUD != null)
            _gameplayHUD.SetActive(true);

        if (_globalHUD != null)
            _globalHUD.SetActive(false);
    }

    public void FinishRun()
    {
        if (_runFinished)
            return;

        _runFinished = true;

        long finalScore = _scoreManager.Score;

        RewardConfig newRecordReward =
            _recordManager?.CheckRecord();

        long bestScore =
            _recordManager != null
                ? _recordManager.BestScore
                : finalScore;

        if (_gameplayHUD != null)
            _gameplayHUD.SetActive(false);

        if (_globalHUD != null)
            _globalHUD.SetActive(true);

        _resultsPanelView?.Show(
            finalScore,
            bestScore,
            newRecordReward
        );
    }

    public void StartNewRun()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}