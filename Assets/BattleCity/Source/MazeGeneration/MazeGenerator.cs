using System;
using System.Collections.Generic;
using System.Linq;
using BattleCity.Source.Infrastructure.EntryPoints;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleCity.Source.MazeGeneration
{
    public class MazeCellView : MonoBehaviour
    {
    }

    public enum CellType
    {
        Floor = 0,
        Wall = 1,
        DestructableWall = 2,
        PlayerBase = 3,
    }

    public class MazeCell
    {
        public event Action<int> OnCellHealthChanged;
        public event Action<MazeCell> OnCellTypeChanged;
        public int Health { get; set; }
        public Vector2 CellScale { get; set; }
        public Vector2Int CellCoords { get; set; }
        public CellType CellType { get; set; }

        private void SetCellType(CellType cellType)
        {
            CellType = cellType;
            OnCellTypeChanged?.Invoke(this);
        }

        public void TakeDamage(int amount)
        {
            if (Health <= 0) return;
            
            Health -= amount;
            if (Health <= 0)
            {
                ChangeCellToFloor();
            }

            OnCellHealthChanged?.Invoke(Health);
        }

        private void ChangeCellToFloor()
        {
            SetCellType(CellType.Floor);
            Health = -1;
        }
    }


    public class CellFactory
    {
        private readonly TilePreset[] _tilePresets;

        public CellFactory(TilePreset[] tilePresets)
        {
            _tilePresets = tilePresets;
        }

        public MazeCell CreateCellData(Vector2Int cellCoords, CellType cellType)
        {
            TilePreset tilePreset = _tilePresets.First(x => x.CellType == cellType);
            return new MazeCell()
            {
                CellType = cellType,
                CellCoords = cellCoords,
                Health = tilePreset.Health,
                CellScale = tilePreset.CellScale,
            };
        }
    }

    public class MazeGenerator : IMazeGenerator
    {
        private MazeConfiguration _mazeConfig;
        private CellFactory _cellFactory;

        public MazeCell[,] GenerateMaze(CellFactory cellFactory, MazeConfiguration mazeConfiguration)
        {
            _mazeConfig = mazeConfiguration;
            _cellFactory = cellFactory;

            MazeCell[,] maze = new MazeCell[_mazeConfig.MazeSize.x, _mazeConfig.MazeSize.y];
            List<Vector2Int> walkablePath = GenerateRandomPath(new Vector2Int(0, 0),
                new Vector2Int(_mazeConfig.MazeSize.x, _mazeConfig.MazeSize.y), mazeConfiguration.PathSpread);

            for (int i = 0; i < _mazeConfig.MazeSize.x; i++)
            {
                for (int j = 0; j < _mazeConfig.MazeSize.y; j++)
                {
                    PopulateCells(maze, walkablePath, i, j);
                }
            }


            maze[0, 0] = _cellFactory.CreateCellData(new Vector2Int(0, 0), CellType.PlayerBase);

            return maze;
        }

        private List<Vector2Int> GenerateRandomPath(Vector2Int start, Vector2Int destination, int spread)
        {
            Vector2Int current = start;
            List<Vector2Int> path = new List<Vector2Int>();
            for (var i = 0; i < spread; i++)
            {
                Vector2Int checkPoint = new Vector2Int(Random.Range(0, _mazeConfig.MazeSize.x),
                    Random.Range(0, _mazeConfig.MazeSize.y));

                path.AddRange(GenerateClosestPath(current, checkPoint));
                current = checkPoint;
            }

            path.AddRange(GenerateClosestPath(current, destination));
            return path;
        }

        private void PopulateCells(MazeCell[,] maze, List<Vector2Int> walkablePath, int x, int y)
        {
            if (walkablePath.Contains(new Vector2Int(x, y)))
            {
                if (Random.value > 1 - _mazeConfig.DestructableWallProbability)
                {
                    maze[x, y] = _cellFactory.CreateCellData(new Vector2Int(x, y), CellType.DestructableWall);
                }
                else
                {
                    maze[x, y] = _cellFactory.CreateCellData(new Vector2Int(x, y), CellType.Floor);
                }
            }
            else
            {
                maze[x, y] = _cellFactory.CreateCellData(new Vector2Int(x, y), CellType.Wall);
            }
        }

        private List<Vector2Int> GenerateClosestPath(Vector2Int start, Vector2Int destination)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            Vector2Int currentCell = start;
            path.Add(currentCell);

            while (true)
            {
                List<Vector2Int> neighbours = GetNeighbours(currentCell);
                Vector2Int bestNeighbour = neighbours.OrderBy(x => Vector2.Distance(x, destination)).First();

                path.Add(bestNeighbour);
                currentCell = bestNeighbour;

                if (bestNeighbour == destination)
                {
                    return path;
                }
            }
        }

        private List<Vector2Int> GetNeighbours(Vector2Int cell)
        {
            List<Vector2Int> neighbours = new List<Vector2Int>(4);

            // Top neighbor
            Vector2Int topNeighbor = new Vector2Int(cell.x, cell.y + 1);
            if (IsCellValid(topNeighbor))
                neighbours.Add(topNeighbor);

            // Right neighbor
            Vector2Int rightNeighbor = new Vector2Int(cell.x + 1, cell.y);
            if (IsCellValid(rightNeighbor))
                neighbours.Add(rightNeighbor);

            // Bottom neighbor
            Vector2Int bottomNeighbor = new Vector2Int(cell.x, cell.y - 1);
            if (IsCellValid(bottomNeighbor))
                neighbours.Add(bottomNeighbor);

            // Left neighbor
            Vector2Int leftNeighbor = new Vector2Int(cell.x - 1, cell.y);
            if (IsCellValid(leftNeighbor))
                neighbours.Add(leftNeighbor);


            return neighbours;
        }

        private bool IsCellValid(Vector2Int coords)
        {
            return coords.x > 0 || coords.y > 0 || coords.x < _mazeConfig.MazeSize.x - 1 ||
                   coords.y < _mazeConfig.MazeSize.y - 1;
        }
    }
}