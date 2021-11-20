using UnityEngine;

namespace Assets.Scripts.Ball
{
    public class BallMovementComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _currentSpeed;
        private Vector3 _movementDirection;

        public void SetNewMovementSpeed(Vector3 newSpeed)
        {
            _currentSpeed = newSpeed;
        }

        public void SetRandomMovementDirection()
        {
            _movementDirection = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f));
            _movementDirection = _movementDirection.normalized;
        }

        public void ResetMovement()
        {
            _currentSpeed = Constants.DefaultBallSpeed;
            _movementDirection = Vector3.zero;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(transform.position + Vector3.Scale(_currentSpeed, _movementDirection) * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Vector3 rotateDirection = (collision.GetContact(0).point - transform.position) * -1;
            _movementDirection = Vector3.Scale(_movementDirection, rotateDirection).normalized;
        }
    }
}
