using System.Collections.Generic;
using System.Linq;
using BattleCity.Source.Enemies;
using BattleCity.Source.Infrastructure.Services.GameFactory;
using BattleCity.Source.Infrastructure.Services.MazeService;
using BattleCity.Source.MazeGeneration;
using BattleCity.Source.PlayerLogic;
using UnityEngine;
using VContainer;

namespace BattleCity.Source.Infrastructure.Services.EnemyDirector
{
    public class EnemyDirector : IEnemyDirector
    {
        private IGameFactory _gameFactory;
        private IMazeManager _mazeManager;

        private readonly List<Enemy> _enemies = new();
        
        [Inject]
        public void Construct(IGameFactory gameFactory, IMazeManager mazeManager)
        {
            _mazeManager = mazeManager;
            _gameFactory = gameFactory;
        }
        
        public void Initialize()
        {
            
        }

        public void PrepareEnemies(Player player)
        {
            IEnumerable<MazeCell> freeCells = _mazeManager.Maze
                .Flatten()
                .Where(x => x.CellType == CellType.Floor)
                .OrderByDescending(x => x.CellCoords, new Vector2IntComparer());
            
            List<MazeCell> enemySpawnPoints = freeCells.Take(4).ToList();
            
            foreach (MazeCell enemySpawnPoint in enemySpawnPoints)
            {
                Vector2Int cellCoords = new Vector2Int(enemySpawnPoint.CellCoords.x, enemySpawnPoint.CellCoords.y);
                Enemy enemy = _gameFactory.CreateEnemy(cellCoords);
                _enemies.Add(enemy);
                enemy.GetComponent<EnemyAI>().SetFollowTarget(player.transform);
            }
        }
    }
}