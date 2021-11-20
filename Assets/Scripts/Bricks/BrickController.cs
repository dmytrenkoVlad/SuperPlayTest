using Assets.Scripts.Ball;
using Assets.Scripts.Effects;
using System;
using UnityEngine;

namespace Assets.Scripts.Bricks
{
    public class BrickController : MonoBehaviour
    {
        public event Action<BrickController> BrickDestroyed = delegate (BrickController brickController) { };
        public event Action<int> BrickDamaged = delegate (int newHealth) { };

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.TryGetComponent(out BallController ball))
                return;
            if (!TryGetComponent(out HealthComponent healthComponent))
                return;
            if (!TryGetComponent(out OnDestroyBaseEffectComponent onDestroyEffect))
                return;

            healthComponent.DecreaseHealth(ball.CurrentDamage);

            if (healthComponent.IsAlive)
            {
                BrickDamaged(healthComponent.CurrentHealth);
            }
            else
            {
                onDestroyEffect.ApplyOnDestroyEffect(ball);
                BrickDestroyed(this);
            }
        }
    }
}
