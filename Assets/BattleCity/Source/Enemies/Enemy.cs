using UnityEngine;

namespace BattleCity.Source.Enemies
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public void TakeDamage(int amount)
        {
            Destroy(gameObject);
        }
    }

    public interface IDamageable
    {
        void TakeDamage(int amount);
    }
}