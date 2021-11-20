using Assets.Scripts.Ball;
using UnityEngine;

namespace Assets.Scripts.Effects
{
    public abstract class OnDestroyBaseEffectComponent : MonoBehaviour
    {
        public abstract void ApplyOnDestroyEffect(BallController ballController);
    }
}
