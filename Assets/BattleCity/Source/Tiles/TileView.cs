using System;
using System.Linq;
using BattleCity.Source.Enemies;
using BattleCity.Source.Infrastructure.Services.MazeService;
using BattleCity.Source.MazeGeneration;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class TileView : MonoBehaviour
{
    private IMazeManager _mazeManager;
    private MazeConfiguration _mazeConfiguration;
    public MazeCell Cell { get; set; }
    public abstract CellType CellType { get;}
    

    public void Initialize(MazeConfiguration mazeConfiguration, MazeCell  cell)
    {
        Cell = cell;
        GetComponent<SpriteRenderer>().sprite = mazeConfiguration.TilePresets.First(x => x.CellType == CellType).Sprite;
    }

 

  
}