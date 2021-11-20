using Assets.Scripts.Bricks;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _defaultBrickDamagedSound;
        [SerializeField] private AudioClip _specialBrickDamagedSound;
        [SerializeField] private AudioClip _brickDestroyedSound;
        [SerializeField] private AudioClip _ambientSound;
        [SerializeField] private AudioClip _victorySound;
        [SerializeField] private AudioClip _lostSound;

        [SerializeField] private BrickManager _brickManager;
        [SerializeField] private LevelController _levelController;

        [SerializeField] private AudioSource _ambientSource;
        [SerializeField] private AudioSource _damageSource;
        [SerializeField] private AudioSource _levelFinishedSource;

        private void Awake()
        {
            _brickManager.BrickDestroyed += OnBrickDestroyed;
            _brickManager.BrickDamaged += OnBrickDamaged;

            _levelController.LevelCompleted += OnLevelCompleted;
            _levelController.LevelLost += OnLevelLost;
        }

        private void Start()
        {
            PlaySound(_ambientSource, _ambientSound);
        }

        private void OnLevelCompleted()
        {
            PlaySound(_levelFinishedSource, _victorySound);
        }

        private void OnLevelLost()
        {
            PlaySound(_levelFinishedSource, _lostSound);
        }

        private void OnBrickDestroyed()
        {
            PlaySound(_damageSource, _brickDestroyedSound);
        }

        private void OnBrickDamaged(BrickType type)
        {
            AudioClip clip = type == BrickType.Default ? _defaultBrickDamagedSound : _specialBrickDamagedSound;
            PlaySound(_damageSource, clip);
        }

        private void PlaySound(AudioSource source, AudioClip clip)
        {
            source.clip = clip;
            source.Play();
        }

        private void OnDestroy()
        {
            _brickManager.BrickDestroyed -= OnBrickDestroyed;
            _brickManager.BrickDamaged -= OnBrickDamaged;

            _levelController.LevelCompleted -= OnLevelCompleted;
            _levelController.LevelLost -= OnLevelLost;
        }
    }
}
