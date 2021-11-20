using UnityEngine;

namespace Assets.Scripts.Bricks
{
    public class HealthComponent : MonoBehaviour
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public bool IsAlive => CurrentHealth > 0;

        public void Init(int health)
        {
            MaxHealth = health;
            CurrentHealth = health;
        }

        public void DecreaseHealth(int damage)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        }

        public void ResetHealth()
        {
            CurrentHealth = MaxHealth;
        }
    }
}
