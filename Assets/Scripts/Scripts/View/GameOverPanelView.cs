using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelView : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;

    [Header("Buttons")]
    [SerializeField] private Button _addThirtySecondsButton;
    [SerializeField] private Button _continueAdButton;

    [Header("Managers")]
    [SerializeField] private ScoreManager _scoreManager;

    private void Awake()
    {
        _addThirtySecondsButton.onClick.AddListener(OnAddThirtySecondsClicked);
        _continueAdButton.onClick.AddListener(OnContinueAfterAdClicked);

    }

    private void OnDestroy()
    {
        _addThirtySecondsButton.onClick.RemoveListener(OnAddThirtySecondsClicked);
        _continueAdButton.onClick.RemoveListener(OnContinueAfterAdClicked);
    }

    public void Show()
    {
        Debug.Log("Show Panel");     
        _finalScoreText.text = $" Your Score\n{_scoreManager.Score}";

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnAddThirtySecondsClicked()
    {
        Hide();
        GameTimer.Instance.AddThirtySeconds();
    }

    private void OnContinueAfterAdClicked()
    {
        Hide();

        // Поки що без реклами.
        // Тут потім буде AdsManager.
        GameTimer.Instance.ContinueAfterAd();
    }
}