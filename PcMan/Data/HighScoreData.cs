using PcMan.Model;
using System.Collections.Generic;

/// <summary>
/// HighScoreData class manages high scores for the game.
/// </summary>
public class HighScoreData
{
    private List<HighScoreEntry> highScores;

    // TODO: Replace hardcoded high scores with a Entity Framework Core database
    /// <summary>
    /// Initializes a new instance of the HighScoreData class and sets up the initial high scores.
    /// </summary>
    public HighScoreData()
    {
        // Set up 10 fake high scores
        highScores = new List<HighScoreEntry>();
        highScores.Add(new HighScoreEntry("AAA", 1000, 20));
        highScores.Add(new HighScoreEntry("BBB", 900, 19));
        highScores.Add(new HighScoreEntry("CCC", 800, 18));
        highScores.Add(new HighScoreEntry("DDD", 700, 17));
        highScores.Add(new HighScoreEntry("EEE", 600, 16));
        highScores.Add(new HighScoreEntry("FFF", 500, 15));
        highScores.Add(new HighScoreEntry("GGG", 400, 14));
        highScores.Add(new HighScoreEntry("HHH", 300, 13));
        highScores.Add(new HighScoreEntry("III", 200, 12));
        highScores.Add(new HighScoreEntry("JJJ", 100, 11));
    }

    /// <summary>
    /// Returns the list of high score entries.
    /// </summary>
    /// <returns>A List of HighScoreEntry objects representing the high scores.</returns>
    internal List<HighScoreEntry> GetHighScores()
    {
        return highScores;
    }

    /// <summary>
    /// Returns the lowest high score.
    /// </summary>
    /// <returns>An integer representing the lowest high score.</returns>
    public int GetLowestScore()
    {
        int lowestScore = int.MaxValue;
        foreach (var entry in highScores)
        {
            if (entry.Score < lowestScore)
            {
                lowestScore = entry.Score;
            }
        }
        return lowestScore;
    }

    /// <summary>
    /// Adds a new high score entry and updates the high scores list.
    /// </summary>
    /// <param name="entry">A HighScoreEntry object representing the new high score entry.</param>
    internal void AddHighScore(HighScoreEntry entry)
    {
        highScores.Add(entry);
        highScores.Sort((a, b) => b.Score.CompareTo(a.Score)); // sort in descending order
        highScores.RemoveAt(highScores.Count - 1); // remove the lowest score
    }
}