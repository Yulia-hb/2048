using UnityEngine;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

namespace ChainCube.Scripts.Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private string _leaderboardId = "Global_Score";

        public async void SubmitScore(long score)
        {
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.LogError("Player is not signed in!");
                return;
            }

            try
            {
                var entry = await LeaderboardsService.Instance
                    .AddPlayerScoreAsync(_leaderboardId, score);

                Debug.Log(
                    $"Score submitted: {entry.Score}, Rank: {entry.Rank}");
            }
            catch (System.Exception exception)
            {
                Debug.LogError(
                    $"Leaderboard error: {exception.Message}");
            }
        }
    }
}