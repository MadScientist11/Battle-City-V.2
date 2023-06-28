using BattleCity.Source.MazeGeneration;
using BattleCity.Source.PlayerLogic;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        MazeView CreateMazeView();
        Player CreatePlayer(Vector3 position);
        Projectile GetOrCreateProjectile(GameObject projectileShooter, Vector3 position, Quaternion rotation);
        TileView CreateTile(MazeCell cell, Transform parent);
    }
}