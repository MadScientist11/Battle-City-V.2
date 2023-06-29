using System;
using BattleCity.Source.Infrastructure.Services.MazeService;
using BattleCity.Source.MazeGeneration;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class TileView : MonoBehaviour
{
    protected IMazeManager _mazeManager;
    public MazeCell Cell { get; set; }
    public abstract CellType CellType { get;}



    public void Initialize(MazeCell  cell)
    {
        Cell = cell;
    }

    public abstract void HealthChanged(int hp);





}