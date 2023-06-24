using BattleCity.Source.Infrastructure.EntryPoints;
using BattleCity.Source.MazeGeneration;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.MazeService
{
    public class MazeManager : IMazeManager
    {
        private readonly IMazeGenerator _mazeGenerator;
        private readonly MazeConfiguration _mazeConfiguration;
        private MazeCellData[,] _maze;

        public MazeCellData[,] Maze => _maze;


        public MazeManager(IMazeGenerator mazeGenerator, MazeConfiguration mazeConfiguration)
        {
            _mazeGenerator = mazeGenerator;
            _mazeConfiguration = mazeConfiguration;
        }

        public void Initialize()
        {
        }

        public void CreateMazeModel()
        {
            _maze = _mazeGenerator.GenerateMaze(_mazeConfiguration);
        }

        public Vector3 GetPlayerStartPosition()
        {
            return new Vector3(_maze[0, 0].CellCoords.x, _maze[0, 0].CellCoords.y, -1);
        }
    }
}