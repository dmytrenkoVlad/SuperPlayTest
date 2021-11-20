using Assets.Scripts.Bricks;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private BrickManager _brickManager;

        public int ScoreForPlaySession { get; private set; }
        public int MaxScore { get; private set; }

        public event Action<int> ScoreUpdated = delegate (int newScore) { };

        private void Awake()
        {
            _brickManager.BrickDestroyed += OnBrickDestroyed;
            MaxScore = PlayerPrefsManager.GetPlayerMaxScore();
        }

        private void OnBrickDestroyed()
        {
            ScoreForPlaySession++;
            TryUpdateMaxScore();
            ScoreUpdated(ScoreForPlaySession);
        }

        public void ResetScore()
        {
            ScoreForPlaySession = 0;
            ScoreUpdated(ScoreForPlaySession);
        }

        private void TryUpdateMaxScore()
        {
            if (MaxScore < ScoreForPlaySession)
            {
                MaxScore = ScoreForPlaySession;
                PlayerPrefsManager.SetPlayerMaxScore(MaxScore);
            }
        }

        private void OnDestroy()
        {
            _brickManager.BrickDestroyed -= OnBrickDestroyed;
        }
    }
}
