using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ChainCube.Scripts.Rewards;

public class ResultsPanelView : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    [Header("New Record")]
    [SerializeField] private GameObject _newRecordContainer;
    [SerializeField] private TextMeshProUGUI _newRecordText;
    [SerializeField] private TextMeshProUGUI _gemsRewardText;
    [SerializeField] private TextMeshProUGUI _coinsRewardText;

    [Header("Buttons")]
    [SerializeField] private Button _newRunButton;

    private GameFlowManager _gameFlowManager;

    private void Awake()
    {
        _gameFlowManager = FindObjectOfType<GameFlowManager>();

        _newRunButton.onClick.AddListener(OnNewRunClicked);
    }

    private void OnDestroy()
    {
        _newRunButton.onClick.RemoveListener(OnNewRunClicked);
    }

    public void Show(
        long score,
        long bestScore,
        RewardConfig newRecordReward)
    {
        _scoreText.text = $"YOUR SCORE: {score}";
        _bestScoreText.text = $"BEST SCORE: {bestScore}";

        bool isNewRecord = newRecordReward != null;

        _newRecordContainer.SetActive(isNewRecord);

        if (isNewRecord)
        {
            _newRecordText.text = "NEW RECORD!";

            ShowRewards(newRecordReward);
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowRewards(RewardConfig rewardConfig)
    {
        _gemsRewardText.text = "";
        _coinsRewardText.text = "";

        foreach (Reward reward in rewardConfig.Rewards)
        {
            switch (reward.Currency)
            {
                case CurrencyType.Gems:
                    _gemsRewardText.text = $"GEMS: +{reward.Amount}";
                    break;

                case CurrencyType.Coins:
                    _coinsRewardText.text = $"COINS: +{reward.Amount}";
                    break;
            }
        }
    }

    private void OnNewRunClicked()
    {
        //Hide();

        _gameFlowManager?.StartNewRun();
    }
}