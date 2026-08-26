using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Scripts.Rewards
{
    public class RewardSystem : MonoBehaviour
    {
        [SerializeField] private CurrencyWallet _currencyWallet;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private List<RewardConfig> _rewardConfigs = new();

        public RewardConfig CheckReward(RewardTrigger trigger, long value)
        {
            foreach (RewardConfig rewardConfig in _rewardConfigs)
            {
                if (rewardConfig == null)
                    continue;

                if (rewardConfig.Trigger != trigger)
                    continue;

                if (rewardConfig.RequiredValue != value)
                    continue;

                GiveReward(rewardConfig);

                return rewardConfig;
            }

            return null;
        }

        private void GiveReward(RewardConfig rewardConfig)
        {
            if (rewardConfig == null)
                return;

            foreach (Reward reward in rewardConfig.Rewards)
            {
                switch (reward.Currency)
                {
                    case CurrencyType.Gems:

                        if (_currencyWallet == null)
                        {
                            Debug.LogError(
                                "RewardSystem: CurrencyWallet is not assigned!");

                            continue;
                        }

                        _currencyWallet.AddGems(reward.Amount);
                        break;

                    case CurrencyType.Coins:

                        if (_scoreManager == null)
                        {
                            Debug.LogError(
                                "RewardSystem: ScoreManager is not assigned!");

                            continue;
                        }

                        _scoreManager.AddScore(reward.Amount);
                        break;
                }
            }
        }
    }
}