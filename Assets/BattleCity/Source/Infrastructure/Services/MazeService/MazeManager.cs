using System.Linq;
using BattleCity.Source.Infrastructure.EntryPoints;
using BattleCity.Source.Infrastructure.Services.EnemyDirector;
using BattleCity.Source.Infrastructure.Services.GameFactory;
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
        private MazeView _mazeView;

        public MazeCell[,] Maze => _maze;
        public MazeCell[] Maze1D { get; private set; }


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
            Maze1D = _maze.Flatten().ToArray();
        }

        public void SetView(MazeView mazeView)
        {
            _mazeView = mazeView;
        }


        public Vector3 GetPlayerStartPosition()
        {
            return new Vector3(_maze[0, 0].CellCoords.x, _maze[0, 0].CellCoords.y, -1);
        }

        public Vector3 GetCellPosition(MazeCell cell)
        {
            return new Vector3(cell.CellCoords.x + cell.CellScale.x/2, cell.CellCoords.y + cell.CellScale.y/2,
                0);
        }

        public MazeCell GetCellByPosition(Vector3 position)
        {
            return Maze1D.FirstOrDefault(x => GetCellPosition(x) == position);
        }

        public TileView GetTile(Vector3 position)
        {
            MazeCell cell = GetCellByPosition(position);
            return _mazeView.Tiles[cell.CellCoords.x, cell.CellCoords.y];
        }
    }
}