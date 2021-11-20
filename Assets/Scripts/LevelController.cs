using Assets.Scripts.Ball;
using Assets.Scripts.Bricks;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameObject _ballPrefab;
        [SerializeField] private GameObject _platformPrefab;
        [SerializeField] private BrickSpawner _brickSpawner;
        [SerializeField] private BrickManager _brickManager;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private PlayerController _playerController;

        [SerializeField] private PlayerInput _playerInput;

        [SerializeField] private Vector2 _bottomLeftAreaPosition;
        [SerializeField] private Vector2 _bottomRightAreaPosition;

        private BallController _ball;
        private PlatformController _platform;

        public event Action LevelCompleted = delegate () { };
        public event Action LevelLost = delegate () { };

        private void Awake()
        {
            _brickManager.BrickDestroyed += OnBrickDestroyed;
            _playerController.LostAttempt += OnAttemptLost;
        }

        private void Start()
        {
            StartLevel();
        }

        private void OnAttemptLost()
        {
            if (_playerController.Attempts > 0)
            {
                ResetBallPosition();
            }
            else
            {
                LevelLost();
            }
        }

        private void OnBrickDestroyed()
        {
            if (_brickManager.NonDestroyedBricksCount() == 0)
                LevelCompleted();
        }

        private void StartLevel()
        {
            _brickSpawner.SpawnBricks();
            InitializePlatform();
            InitializeBall();
            _playerController.Init(_ball, _bottomLeftAreaPosition);
        }

        private void ResetBallPosition()
        {
            _platform.transform.position = new Vector2(0, _bottomLeftAreaPosition.y);
            _ball.GetComponent<BallMovementComponent>().ResetMovement(_platform.StartBallPositionTransfrom);
        }

        public void RestartLevel()
        {
            ResetBallPosition();
            _scoreManager.ResetScore();
            _brickManager.ResetBricks();
            _playerController.ResetAttempts();
        }

        private void InitializePlatform()
        {
            _platform = Instantiate(_platformPrefab, new Vector2(0, _bottomLeftAreaPosition.y), Quaternion.identity).GetComponent<PlatformController>();
            _platform.GetComponent<PlatfromMovement>().InitializeMovementBorders(_bottomLeftAreaPosition, _bottomRightAreaPosition);
            _platform.Init(_playerInput);
        }

        private void InitializeBall()
        {
            _ball = Instantiate(_ballPrefab, _platform.StartBallPositionTransfrom.position, Quaternion.identity).GetComponent<BallController>();
            _ball.transform.parent = _platform.StartBallPositionTransfrom;
            _ball.Init(_platform, _playerInput);
        }

        private void OnDestroy()
        {
            _brickManager.BrickDestroyed -= OnBrickDestroyed;
            _playerController.LostAttempt -= OnAttemptLost;
        }
    }
}
