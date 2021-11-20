using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthPanel : MonoBehaviour
    {
        [SerializeField] private Image[] _hearts;
        [SerializeField] private PlayerController _playerController;

        private void Awake()
        {
            _playerController.LostAttempt += OnLostAttempt;
        }

        private void OnLostAttempt()
        {
            foreach (var heart in _hearts)
            {
                if (heart.gameObject.activeSelf)
                {
                    heart.gameObject.SetActive(false);
                    return;
                }
            }
        }

        public void ResetHearts()
        {
            foreach (var heart in _hearts)
            {
                heart.gameObject.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            _playerController.LostAttempt -= OnLostAttempt;
        }
    }
}
