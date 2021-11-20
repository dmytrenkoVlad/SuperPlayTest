using UnityEngine;

namespace Assets.Scripts
{
    public static class PlayerPrefsManager
    {
        private const string ScoreKey = "Score";

        public static int GetPlayerMaxScore()
        {
            return PlayerPrefs.GetInt(ScoreKey);
        }

        public static void SetPlayerMaxScore(int newScore)
        {
            PlayerPrefs.SetInt(ScoreKey, newScore);
        }
    }
}
