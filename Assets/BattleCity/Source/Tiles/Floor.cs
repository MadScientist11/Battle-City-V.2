using BattleCity.Source.MazeGeneration;
using UnityEngine;

public class Floor : TileView
{
    public Vector2Int Coords { get; set; }
    public override CellType CellType => CellType.Floor;
    public override void HealthChanged(int hp)
    {
        
    }
}