using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChainCube.Scripts.Leaderboard
{
    public class LeaderboardPanelView : MonoBehaviour
    {
        [Header("Top Players")]
        [SerializeField] private Transform _entriesContainer;
        [SerializeField] private LeaderboardEntryView _entryPrefab;

        [Header("My Player")]
        [SerializeField] private GameObject _myPlayerContainer;
        [SerializeField] private TextMeshProUGUI _myRankText;
        [SerializeField] private TextMeshProUGUI _myNameText;
        [SerializeField] private TextMeshProUGUI _myScoreText;

        [Header("Buttons")]
        [SerializeField] private Button _closeButton;

        private readonly List<LeaderboardEntryView>
            _spawnedEntries = new();

        private void Awake()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(Hide);
        }

        public void Show(
            List<LeaderboardPlayerData> entries,
            LeaderboardPlayerData myEntry)
        {
            ClearEntries();

            foreach (LeaderboardPlayerData entry in entries)
            {
                LeaderboardEntryView view =
                    Instantiate(
                        _entryPrefab,
                        _entriesContainer
                    );

                view.SetData(
                    entry.Rank,
                    entry.PlayerName,
                    entry.Score
                );

                _spawnedEntries.Add(view);
            }

            ShowMyPlayer(myEntry);

            gameObject.SetActive(true);
        }

        private void ShowMyPlayer(
            LeaderboardPlayerData myEntry)
        {
            if (myEntry == null)
            {
                _myPlayerContainer?.SetActive(false);
                return;
            }

            _myPlayerContainer?.SetActive(true);

            _myRankText.text =
                (myEntry.Rank + 1).ToString();

            _myNameText.text =
                string.IsNullOrEmpty(myEntry.PlayerName)
                    ? "Player"
                    : myEntry.PlayerName;

            _myScoreText.text =
                myEntry.Score.ToString();
        }

        private void ClearEntries()
        {
            foreach (LeaderboardEntryView entry
                     in _spawnedEntries)
            {
                if (entry != null)
                    Destroy(entry.gameObject);
            }

            _spawnedEntries.Clear();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_closeButton != null)
                _closeButton.onClick.RemoveListener(Hide);
        }
    }
}