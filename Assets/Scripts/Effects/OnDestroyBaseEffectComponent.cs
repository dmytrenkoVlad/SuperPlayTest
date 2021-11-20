using Assets.Scripts.Ball;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public abstract class OnDestroyBaseEffectComponent : MonoBehaviour
    {
        protected abstract float EffectDuration { get; }
        public abstract void ApplyOnDestroyEffect(BallController ballController, PlatformController platformController);
    }
}
