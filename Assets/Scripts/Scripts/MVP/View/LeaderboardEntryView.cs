using TMPro;
using UnityEngine;

namespace ChainCube.Scripts.Leaderboard
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _rankText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void SetData(
            int rank,
            string playerName,
            long score)
        {
            _rankText.text = (rank + 1).ToString();

            _nameText.text =
                string.IsNullOrEmpty(playerName)
                    ? "Player"
                    : playerName;

            _scoreText.text = score.ToString();
        }
    }
}