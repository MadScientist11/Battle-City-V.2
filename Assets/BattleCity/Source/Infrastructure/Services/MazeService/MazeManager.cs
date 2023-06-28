using BattleCity.Source.Infrastructure.EntryPoints;
using BattleCity.Source.MazeGeneration;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.MazeService
{
    public class MazeManager : IMazeManager
    {
        private readonly IMazeGenerator _mazeGenerator;
        private readonly MazeConfiguration _mazeConfiguration;
        private MazeCell[,] _maze;
        private CellFactory _cellFactory;

        public MazeCell[,] Maze => _maze;


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
            _cellFactory = new CellFactory(_mazeConfiguration.TilePresets);

            _maze = _mazeGenerator.GenerateMaze(_cellFactory, _mazeConfiguration);
        }


        public void UpdateMazeCell(Vector2Int coords, MazeCell  cell)
        {
            _maze[coords.x, coords.y] = cell;
        }


        public Vector3 GetPlayerStartPosition()
        {
            return new Vector3(_maze[0, 0].CellCoords.x, _maze[0, 0].CellCoords.y, -1);
        }
    }
}