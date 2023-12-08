namespace PcMan.Model
{
    internal class HighScoreEntry
    {
        public string PlayerName;
        public int Score;
        public int Level;
        public HighScoreEntry(string name, int score, int level)
        {
            PlayerName = name;
            Score = score;
            Level = level;
        }
    }
}