using UnityEngine;
using ChainCube.Scripts.Rewards;
using ChainCube.Scripts.Leaderboard;

namespace ChainCube.Scripts.Records
{
    public class RecordManager : MonoBehaviour
    {
        private const string BestScoreKey = "BestScore";

        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private RewardSystem _rewardSystem;
        [SerializeField] private LeaderboardManager _leaderboardManager;

        private long _bestScore;

        public long BestScore => _bestScore;

        private void Start()
        {
            _bestScore = GetBestScore();
        }

        public RewardConfig CheckRecord()
        {
            if (_scoreManager == null)
                return null;

            long currentScore = _scoreManager.Score;

            if (currentScore <= _bestScore)
                return null;

            _bestScore = currentScore;

            SaveBestScore();

            _leaderboardManager?.SubmitScore(_bestScore);

            return _rewardSystem?.CheckReward(
                RewardTrigger.NewRecord,
                1);
        }

        private void SaveBestScore()
        {
            PlayerPrefs.SetString(
                BestScoreKey,
                _bestScore.ToString());

            PlayerPrefs.Save();
        }

        private long GetBestScore()
        {
            string savedScore =
                PlayerPrefs.GetString(BestScoreKey, "0");

            if (long.TryParse(savedScore, out long result))
                return result;

            return 0;
        }
    }
}