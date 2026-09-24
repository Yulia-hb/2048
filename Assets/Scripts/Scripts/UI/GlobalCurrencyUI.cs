using TMPro;
using UnityEngine;
using ChainCube.Scripts.Rewards;
using UnityEngine.UI;

public class GlobalCurrencyUI : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private CurrencyWallet _currencyWallet;

    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private TextMeshProUGUI _gemsText;

    [SerializeField] private Image _coin;
    [SerializeField] private Image _crystals;

    private void Start()
    {
        UpdateCoins();
        UpdateGems();

        _currencyWallet.OnCurrencyChanged += OnCurrencyChanged;
    }

    private void Update()
    {
        UpdateCoins();
    }

    private void OnCurrencyChanged(CurrencyType type, long amount)
    {
        if (type == CurrencyType.Gems)
            _gemsText.text = amount.ToString();
    }

    private void UpdateCoins()
    {
        _coinsText.text = _scoreManager.Score.ToString();
    }

    private void UpdateGems()
    {
        _gemsText.text = _currencyWallet.Gems.ToString();
    }

    private void OnDestroy()
    {
        if (_currencyWallet != null)
            _currencyWallet.OnCurrencyChanged -= OnCurrencyChanged;
    }
}
