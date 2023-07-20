using BattleCity.Source.MazeGeneration;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.MazeService
{
    public interface IMazeManager : IService
    {
        MazeCell[,] Maze { get; }
        MazeCell[] Maze1D { get; }
        void CreateMazeModel();
        Vector3 GetPlayerStartPosition();
        Vector3 GetCellPosition(MazeCell cell);
        MazeCell GetCellByPosition(Vector3 position);
        TileView GetTile(Vector3 position);
        void SetView(MazeView mazeView);
    }
}