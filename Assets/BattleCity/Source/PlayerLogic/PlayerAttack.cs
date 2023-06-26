using BattleCity.Source.Infrastructure.Services.GameFactory;
using UnityEngine;
using VContainer;

namespace BattleCity.Source.PlayerLogic
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _projectileSpawnPoint;
        private IGameFactory _gameFactory;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        private void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                _gameFactory.GetOrCreateProjectile(_projectileSpawnPoint.position);
            }
        }
    }
}
