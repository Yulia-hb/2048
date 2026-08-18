using System;
using UnityEngine;

namespace ChainCube.Scripts.Rewards
{
    public class CurrencyWallet : MonoBehaviour
    {
        [Header("Starting Gems")]
        [SerializeField] private long _gems;

        public long Gems => _gems;

        public event Action<CurrencyType, long> OnCurrencyChanged;

        public void AddGems(long amount)
        {
            if (amount <= 0)
                return;

            _gems += amount;

            Debug.Log($"CURRENCY WALLET: Added {amount} gems. Total: {_gems}");

            OnCurrencyChanged?.Invoke(
                CurrencyType.Gems,
                _gems);
        }

        public bool TrySpendGems(long amount)
        {
            if (amount <= 0)
                return false;

            if (_gems < amount)
                return false;

            _gems -= amount;

            OnCurrencyChanged?.Invoke(
                CurrencyType.Gems,
                _gems);

            return true;
        }

        public long GetAmount(CurrencyType type)
        {
            return type switch
            {
                CurrencyType.Gems => _gems,
                CurrencyType.Coins => 0,
                _ => 0
            };
        }
    }
}