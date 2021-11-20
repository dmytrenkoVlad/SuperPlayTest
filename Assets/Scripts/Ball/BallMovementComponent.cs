using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Ball
{
    public class BallMovementComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private TrailRenderer trailRenderer;

        private Vector2 _currentSpeed;
        private Vector2 _movementDirection;

        public bool IsMoving => _movementDirection != Vector2.zero;

        private Coroutine _coroutine;

        private IEnumerator SpeedChangeCoroutine(Vector2 newSpeed, float duration)
        {
            _currentSpeed = newSpeed;
            yield return new WaitForSeconds(duration);
            _currentSpeed = Constants.DefaultBallSpeed;
            _coroutine = null;
        }

        public void SetNewMovementSpeed(Vector2 newSpeed, float duration)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(SpeedChangeCoroutine(newSpeed, duration));
        }

        public void SetRandomMovementDirection()
        {
            _movementDirection = new Vector2(Random.Range(0f, 1f), Random.Range(0.3f, 1f));
            _movementDirection = _movementDirection.normalized;
            transform.parent = null;
        }

        public void ResetMovement(Transform startPositionTransform)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _currentSpeed = Constants.DefaultBallSpeed;
            _movementDirection = Vector2.zero;
            transform.position = startPositionTransform.position;
            transform.parent = startPositionTransform;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition((Vector2)transform.position + Vector2.Scale(_currentSpeed, _movementDirection) * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector2 normal = collision.GetContact(0).normal;

            if (collision.gameObject.layer == Constants.PlatfromLayer)
            {
                if (normal.y > 0)
                {
                    float contactPositionX = collision.GetContact(0).point.x;
                    float platformWidth = collision.gameObject.transform.localScale.x * collision.gameObject.GetComponent<BoxCollider2D>().size.x;
                    float platformCenterPositionX = collision.gameObject.transform.position.x;

                    float x = Mathf.Abs(contactPositionX - platformCenterPositionX) / (platformWidth / 2);

                    if (contactPositionX < platformCenterPositionX)
                    {
                        _movementDirection = new Vector2(-1 - x, 1).normalized;
                    }
                    else
                    {
                        _movementDirection = new Vector2(1 + x, 1).normalized;
                    }
                    return;
                }
            }

            if(normal.x != 0 && Mathf.Sign(normal.x) != Mathf.Sign(_movementDirection.x))
                _movementDirection.x *= -1;
            if (normal.y != 0 && Mathf.Sign(normal.y) != Mathf.Sign(_movementDirection.y))
                _movementDirection.y *= -1;
        }
    }
}
