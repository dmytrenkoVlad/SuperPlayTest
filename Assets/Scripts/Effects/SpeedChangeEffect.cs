using Assets.Scripts.Ball;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public class SpeedChangeEffect : OnDestroyBaseEffectComponent
    {
        private static Vector2 ChangeBallSpeed = Constants.DefaultBallSpeed * 0.7f;

        protected override float EffectDuration => 10f;

        public override void ApplyOnDestroyEffect(BallController ballController, PlatformController platformController)
        {
            if (!ballController.TryGetComponent(out BallMovementComponent ballMovement))
                return;

            ballMovement.SetNewMovementSpeed(ChangeBallSpeed, EffectDuration);
        }
    }
}
