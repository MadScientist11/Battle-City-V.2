using System;
using BattleCity.Source.Infrastructure.Services;
using BattleCity.Source.MazeGeneration;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.EntryPoints
{
    public interface IMazeGenerator
    {
        MazeCell[,] GenerateMaze(CellFactory cellFactory, MazeConfiguration mazeConfiguration);
    }

    public class MainEntryPoint : MonoBehaviour
    {
        public MazeConfiguration _mazeConfig;
        public MazeGenerator _mazeGenerator = new();
        public GameObject PlayerPrefab;

        private void Start()
        {
        }
    }
}