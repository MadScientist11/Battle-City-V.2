using BattleCity.Source.MazeGeneration;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.MazeService
{
    public interface IMazeManager : IService
    {
        MazeCell[,] Maze { get; }
        void CreateMazeModel();
        Vector3 GetPlayerStartPosition();
    }
}