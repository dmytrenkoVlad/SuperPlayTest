using Assets.Scripts.Ball;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class SpeedIncreaseEffect : OnDestroyBaseEffectComponent
    {
        private const float SpeedIncreaseDurationSecs = 5f;
        private static Vector3 IncreasedBallSpeed = Constants.DefaultBallSpeed * 2;

        private Coroutine _coroutine;

        public override void ApplyOnDestroyEffect(BallController ballController)
        {
            if (!ballController.TryGetComponent(out BallMovementComponent ballMovement))
                return;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(SpeedChangeCoroutine(ballMovement));
        }

        private IEnumerator SpeedChangeCoroutine(BallMovementComponent ballMovement)
        {
            ballMovement.SetNewMovementSpeed(IncreasedBallSpeed);
            yield return new WaitForSeconds(SpeedIncreaseDurationSecs);
            ballMovement.ResetMovement();
            _coroutine = null;
        }
    }
}
