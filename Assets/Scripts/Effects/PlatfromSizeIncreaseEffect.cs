using Assets.Scripts.Ball;

namespace Assets.Scripts.Effects
{
    public class PlatfromSizeIncreaseEffect : OnDestroyBaseEffectComponent
    {
        private const float PlatfromWidthScaleMultiplier = 2f;

        protected override float EffectDuration => 8f;

        public override void ApplyOnDestroyEffect(BallController ballController, PlatformController platformController)
        {
            platformController.SetNewPlatformWidth(PlatfromWidthScaleMultiplier, EffectDuration);
        }
    }
}
