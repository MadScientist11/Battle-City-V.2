using BattleCity.Source.MazeGeneration;
using BattleCity.Source.PlayerLogic;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        MazeView CreateMazeView();
        void CreateMazeCell(CellType cellType, Transform parent, Vector3 position);
        Player CreatePlayer(Vector3 position);
        Projectile GetOrCreateProjectile(Vector3 position);
    }
}