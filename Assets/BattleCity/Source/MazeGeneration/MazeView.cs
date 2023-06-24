using BattleCity.Source.Infrastructure.Services.MazeService;
using BattleCity.Source.MazeGeneration;
using UnityEngine;
using VContainer;

namespace BattleCity.Source.Infrastructure.Services.GameFactory
{
    public class MazeView : MonoBehaviour
    {
        private IMazeManager _mazeManager;
        private IGameFactory _gameFactory;

        private MazeCellData[,] _maze;
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
            CreateMaze();
        }

        private void CreateMaze()
        {
            int rows = _mazeManager.Maze.GetLength(dimension: 0);
            int columns = _mazeManager.Maze.GetLength(dimension: 1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _gameFactory.CreateMazeCell(_mazeManager.Maze[i,j].CellType, transform, new Vector3(_mazeManager.Maze[i,j].CellCoords.x,_mazeManager.Maze[i,j].CellCoords.y,0));
                }
            }
        }
    }
}