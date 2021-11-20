using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlatformController : MonoBehaviour
    {
        [SerializeField] private Transform _ballChildTransform;
        [SerializeField] private PlatfromMovement _platformMovement;

        public Transform StartBallPositionTransfrom  => _ballChildTransform;

        private PlayerInput _playerInput;
        private float _defaultScaleX;

        private Coroutine _coroutine;

        private IEnumerator ScaleChangeCoroutine(float scaleMultiplier, float duration)
        {
            if(transform.localScale.x == _defaultScaleX)
                transform.localScale = new Vector3(transform.localScale.x * scaleMultiplier, transform.localScale.y, transform.localScale.z);
            yield return new WaitForSeconds(duration);
            transform.localScale = new Vector3(_defaultScaleX, transform.localScale.y, transform.localScale.z);
            _coroutine = null;
        }

        public void SetNewPlatformWidth(float scaleMultiplier, float duration)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ScaleChangeCoroutine(scaleMultiplier, duration));
        }

        public void Init(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.PlayerMovedCursor += OnPlayerMovingCursor;
            _defaultScaleX = transform.localScale.x;
        }

        private void OnPlayerMovingCursor(Vector3 oldPosition, Vector3 newPosition)
        {
            _platformMovement.SetXOffset(newPosition.x - oldPosition.x);
        }

        private void OnDestroy()
        {
            _playerInput.PlayerMovedCursor -= OnPlayerMovingCursor;
        }
    }
}
