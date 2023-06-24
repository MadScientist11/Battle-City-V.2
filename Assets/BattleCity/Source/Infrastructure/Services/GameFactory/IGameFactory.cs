using BattleCity.Source.MazeGeneration;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        MazeView CreateMazeView();
        void CreateMazeCell(CellType cellType, Transform parent, Vector3 position);
        PlayerView CreatePlayer(Vector3 position);
    }
}