using UnityEngine;

namespace Assets.Scripts.Ball
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private BallMovementComponent _movementComponent;

        public int CurrentDamage { get; private set; }

        public PlatformController PlatformController { get; private set; }

        private PlayerInput _playerInput;

        public void Init(PlatformController platformController, PlayerInput playerInput)
        {
            _playerInput = playerInput;
            PlatformController = platformController;
            CurrentDamage = Constants.DefaultDamage;
            _movementComponent.ResetMovement(PlatformController.StartBallPositionTransfrom);

            _playerInput.PlayerClicked += OnPlayerClicked;
        }

        private void OnPlayerClicked()
        {
            if(!_movementComponent.IsMoving)
                _movementComponent.SetRandomMovementDirection();
        }

        private void OnDestroy()
        {
            _playerInput.PlayerClicked -= OnPlayerClicked;
        }
    }
}
