using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private Text _currentScoreText;
        [SerializeField] private Text _maxScoreText;

        private void Awake()
        {
            _scoreManager.ScoreUpdated += OnScoreUpdated;
        }

        private void Start()
        {
            _maxScoreText.text = $"Max: {_scoreManager.MaxScore}";
            _currentScoreText.text = "0";
        }

        private void OnScoreUpdated(int newScore)
        {
            _currentScoreText.text = newScore.ToString();
            _maxScoreText.text = $"Max: {_scoreManager.MaxScore}";
        }

        private void OnDestroy()
        {
            _scoreManager.ScoreUpdated -= OnScoreUpdated;
        }
    }
}
