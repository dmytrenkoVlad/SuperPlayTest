using UnityEngine;

namespace Assets.Scripts.Ball
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private BallMovementComponent _movementComponent;
        public int CurrentDamage { get; private set; }

        public void Init()
        {
            CurrentDamage = Constants.DefaultDamage;
            _movementComponent.ResetMovement();
        }
    }
}
