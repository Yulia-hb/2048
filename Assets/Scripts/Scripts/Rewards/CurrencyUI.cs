using TMPro;
using UnityEngine;

namespace ChainCube.Scripts.Rewards
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private CurrencyWallet _currencyWallet;
        [SerializeField] private TextMeshProUGUI _gemsText;

        private void OnEnable()
        {
            if (_currencyWallet == null)
            {
                Debug.LogError("CurrencyUI: CurrencyWallet is not assigned!");
                return;
            }

            if (_gemsText == null)
            {
                Debug.LogError("CurrencyUI: Gems Text is not assigned!");
                return;
            }

            _currencyWallet.OnCurrencyChanged += OnCurrencyChanged;

            UpdateGemsUI();
        }

        private void OnCurrencyChanged(CurrencyType type, long amount)
        {
            Debug.Log($"CurrencyUI received: {type} = {amount}");

            if (type != CurrencyType.Gems)
                return;

            _gemsText.text = amount.ToString();
        }

        private void UpdateGemsUI()
        {
            _gemsText.text =
                _currencyWallet.GetAmount(CurrencyType.Gems).ToString();
        }

        private void OnDisable()
        {
            if (_currencyWallet != null)
                _currencyWallet.OnCurrencyChanged -= OnCurrencyChanged;
        }
    }
}