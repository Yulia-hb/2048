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
    [SerializeField] private TextMeshProUGUI _rewardsText;

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
        _scoreText.text = $"Your Score\n{score}";
        _bestScoreText.text = $"Best Score\n{bestScore}";

        bool isNewRecord = newRecordReward != null;

        _newRecordContainer.SetActive(isNewRecord);

        if (isNewRecord)
        {
            _newRecordText.text = "New Record!";
            _rewardsText.text = BuildRewardText(newRecordReward);
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private string BuildRewardText(RewardConfig rewardConfig)
    {
        string text = "";

        foreach (Reward reward in rewardConfig.Rewards)
        {
            string currencyName = reward.Currency switch
            {
                CurrencyType.Gems => "Gems",
                CurrencyType.Coins => "Coins",
                _ => reward.Currency.ToString()
            };

            if (!string.IsNullOrEmpty(text))
                text += "\n";

            text += $"+{reward.Amount} {currencyName}";
        }

        return text;
    }

    private void OnNewRunClicked()
    {
        Hide();

        _gameFlowManager?.StartNewRun();
    }
}
