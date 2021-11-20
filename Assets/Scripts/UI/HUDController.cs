using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Text _panelHeader;

        [SerializeField] private LevelController _levelController;
        [SerializeField] private HealthPanel _healthPanel;

        private void Awake()
        {
            _pausePanel.SetActive(false);
            _pauseButton.onClick.AddListener(OnPauseButtonClicked);
            _resumeButton.onClick.AddListener(OnResumeClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);

            _levelController.LevelCompleted += ShowVictoryScreen;
            _levelController.LevelLost += ShowDefeatScreen;
        }

        private void OnPauseButtonClicked()
        {
            _pausePanel.SetActive(true);
            _resumeButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(true);
            _panelHeader.text = "Pause";
            Time.timeScale = 0;
        }

        private void OnResumeClicked()
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
        }

        private void ShowVictoryScreen()
        {
            _panelHeader.text = "You Won!";
            _pausePanel.SetActive(true);
            _resumeButton.gameObject.SetActive(false);
        }

        private void ShowDefeatScreen()
        {
            _panelHeader.text = "Wasted!";
            _pausePanel.SetActive(true);
            _resumeButton.gameObject.SetActive(false);
        }

        private void OnRestartButtonClicked()
        {
            _pausePanel.SetActive(false);
            _levelController.RestartLevel();
            _healthPanel.ResetHearts();
            Time.timeScale = 1;
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClicked);
            _resumeButton.onClick.RemoveListener(OnResumeClicked);
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);

            _levelController.LevelCompleted -= ShowVictoryScreen;
            _levelController.LevelLost -= ShowDefeatScreen;
        }
    }
}
