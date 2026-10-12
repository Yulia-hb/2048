using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;

namespace ChainCube.Scripts.Leaderboard
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField]
        private string _leaderboardId = "Global_Score";

        [SerializeField]
        private LeaderboardPanelView _panelView;

        private bool _isLoading;

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
            catch (Exception exception)
            {
                Debug.LogError(
                    $"Leaderboard error: {exception.Message}");
            }
        }

        public async void OpenLeaderboard()
        {
            if (_isLoading)
                return;

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.LogWarning("Player is not signed in!");
                return;
            }

            if (_panelView == null)
            {
                Debug.LogError("LeaderboardPanelView is not assigned!");
                return;
            }

            _isLoading = true;

            try
            {
                await LoadLeaderboard();
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"Leaderboard loading error: {exception.Message}");
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task LoadLeaderboard()
        {
            var response = await LeaderboardsService.Instance
                .GetScoresAsync(
                    _leaderboardId,
                    new GetScoresOptions
                    {
                        Offset = 0,
                        Limit = 10
                    });

            List<LeaderboardPlayerData> players = new();

            foreach (LeaderboardEntry entry in response.Results)
            {
                players.Add(
                    new LeaderboardPlayerData(
                        entry.Rank,
                        entry.PlayerName,
                        (long)entry.Score
                    ));
            }

            LeaderboardPlayerData myPlayer = null;

            try
            {
                var myEntry = await LeaderboardsService.Instance
                    .GetPlayerScoreAsync(_leaderboardId);

                if (myEntry != null)
                {
                    myPlayer = new LeaderboardPlayerData(
                        myEntry.Rank,
                        myEntry.PlayerName,
                        (long)myEntry.Score
                    );
                }
            }
            catch (Exception exception)
            {
                Debug.LogWarning(
                    $"Player score unavailable: {exception.Message}");
            }

            if (_panelView != null)
                _panelView.Show(players, myPlayer);
        }
    }
}
