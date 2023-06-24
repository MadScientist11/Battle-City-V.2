using System;
using BattleCity.Source.MazeGeneration;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.EntryPoints
{
    public interface IMazeGenerator
    {
        MazeCellData[,] GenerateMaze(MazeConfiguration mazeConfiguration);
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