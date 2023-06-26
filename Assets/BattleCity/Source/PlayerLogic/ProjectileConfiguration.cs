using UnityEngine;

namespace BattleCity.Source.PlayerLogic
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Configs/Projectile")]
    public class ProjectileConfiguration : ScriptableObject
    {
        public float ForceMultiplier;
    }
}