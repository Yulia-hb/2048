namespace ChainCube.Scripts.Leaderboard
{
    public class LeaderboardPlayerData
    {
        public int Rank { get; }
        public string PlayerName { get; }
        public long Score { get; }

        public LeaderboardPlayerData(
            int rank,
            string playerName,
            long score)
        {
            Rank = rank;
            PlayerName = playerName;
            Score = score;
        }
    }
}