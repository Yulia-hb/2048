using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Scripts.Rewards
{
    public enum RewardTrigger
    {
        Merge,
        ReachValue,
        NewRecord
    }

    [Serializable]
    public class Reward
    {
        [SerializeField] private CurrencyType _currency;
        [SerializeField] private long _amount;

        public CurrencyType Currency => _currency;
        public long Amount => _amount;
    }

    [CreateAssetMenu(
        fileName = "RewardConfig",
        menuName = "ChainCube/Rewards/Reward Config")]
    public class RewardConfig : ScriptableObject
    {
        [Header("Trigger")]
        [SerializeField] private RewardTrigger _trigger;

        [Header("Condition")]
        [SerializeField] private long _requiredValue;

        [Header("Rewards")]
        [SerializeField] private List<Reward> _rewards = new();

        public RewardTrigger Trigger => _trigger;
        public long RequiredValue => _requiredValue;
        public IReadOnlyList<Reward> Rewards => _rewards;
    }
}
