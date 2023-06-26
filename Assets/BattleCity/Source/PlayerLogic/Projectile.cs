using System;
using BattleCity.Source.Enemies;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using VContainer;

namespace BattleCity.Source.PlayerLogic
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private ProjectileConfiguration _projectileConfig;

        [Inject]
        public void Construct(ProjectileConfiguration projectileConfig)
        {
            _projectileConfig = projectileConfig;
        }

        private void OnEnable()
        {
            _rigidbody2D.AddForce(transform.up * _projectileConfig.ForceMultiplier, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(1);
                Destroy(gameObject);
                return;
            }


            if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
}