using BattleCity.Source.MazeGeneration;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.MazeService
{
    public interface IMazeManager : IService
    {
        MazeCellData[,] Maze { get; }
        void CreateMazeModel();
        Vector3 GetPlayerStartPosition();
    }
}