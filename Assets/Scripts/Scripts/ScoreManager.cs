using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private long _score;

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(long value)
    {
        _score += value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _scoreText.text = _score.ToString();
    }
}