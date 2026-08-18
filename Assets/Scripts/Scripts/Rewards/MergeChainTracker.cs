using UnityEngine;
using ChainCube.Scripts.Utils;

namespace ChainCube.Scripts.Rewards
{
    public class MergeChainTracker : MonoBehaviour
    {
        [SerializeField] private RewardSystem _rewardSystem;

        private ISwipeDetector _swipeDetector;
        private int _mergeCount;

        private void Start()
        {
            _swipeDetector = FindObjectOfType<MouseSwipeDetector>();

            if (_swipeDetector == null)
            {
                Debug.LogError("MergeChainTracker: SwipeDetector not found!");
                return;
            }

            _swipeDetector.onSwipeEnd += OnSwipeEnd;
        }

        private void OnSwipeEnd(Vector2 delta)
        {
            _mergeCount = 0;
        }

        public void RegisterMerge()
        {
            _mergeCount++;

            if (_mergeCount != 3)
                return;

            _rewardSystem?.CheckReward(
                RewardTrigger.Merge,
                3);
        }

        private void OnDestroy()
        {
            if (_swipeDetector != null)
                _swipeDetector.onSwipeEnd -= OnSwipeEnd;
        }
    }
}