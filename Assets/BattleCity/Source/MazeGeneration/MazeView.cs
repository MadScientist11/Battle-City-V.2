using BattleCity.Source.Infrastructure.Services.GameFactory;
using BattleCity.Source.Infrastructure.Services.MazeService;
using UnityEngine;
using VContainer;

namespace BattleCity.Source.MazeGeneration
{
    public class MazeView : MonoBehaviour
    {
        private IMazeManager _mazeManager;
        private IGameFactory _gameFactory;

        private MazeCell[,] _maze;
        public TileView[,] Tiles { get; private set; }

        
        private MazeConfiguration _mazeConfiguration;

        [Inject]
        public void Construct(IMazeManager mazeManager, IGameFactory gameFactory, MazeConfiguration mazeConfiguration)
        {
            _gameFactory = gameFactory;
            _mazeConfiguration = mazeConfiguration;
            _mazeManager = mazeManager;
        }

        public void Initialize()
        {
            CreateTiles();
        }

        private void CreateTiles()
        {
            int rows = _mazeManager.Maze.GetLength(dimension: 0);
            int columns = _mazeManager.Maze.GetLength(dimension: 1);
            Tiles = new TileView[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Tiles[i, j] = _gameFactory
                        .CreateTile(_mazeManager.Maze[i, j], transform);
                    _mazeManager.Maze[i, j].OnCellTypeChanged += ReplaceTile;
                    _mazeManager.Maze[i, j].OnCellHealthChanged += Tiles[i, j].HealthChanged;
                }
            }
        }

        public void OnDestroy()
        {
            int rows = _mazeManager.Maze.GetLength(dimension: 0);
            int columns = _mazeManager.Maze.GetLength(dimension: 1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Destroy(Tiles[i, j].gameObject);
                    _mazeManager.Maze[i, j].OnCellTypeChanged -= ReplaceTile;
                    _mazeManager.Maze[i, j].OnCellHealthChanged -= Tiles[i, j].HealthChanged;

                }
            }
        }

        private void ReplaceTile(MazeCell cell)
        { 
            Destroy(Tiles[cell.CellCoords.x, cell.CellCoords.y].gameObject);
            Tiles[cell.CellCoords.x, cell.CellCoords.y] = _gameFactory
                .CreateTile(_mazeManager.Maze[cell.CellCoords.x, cell.CellCoords.y], transform);
        }
    }
}