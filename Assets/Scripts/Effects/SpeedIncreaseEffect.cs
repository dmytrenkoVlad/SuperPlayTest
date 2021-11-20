using Assets.Scripts.Ball;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class SpeedIncreaseEffect : OnDestroyBaseEffectComponent
    {
        private static Vector2 IncreasedBallSpeed = Constants.DefaultBallSpeed * 1.5f;

        protected override float EffectDuration => 10f;

        public override void ApplyOnDestroyEffect(BallController ballController, PlatformController platformController)
        {
            if (!ballController.TryGetComponent(out BallMovementComponent ballMovement))
                return;

            ballMovement.SetNewMovementSpeed(IncreasedBallSpeed, EffectDuration);
        }
    }
}
