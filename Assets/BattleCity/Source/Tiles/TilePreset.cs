using BattleCity.Source.MazeGeneration;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/TilePreset", fileName = "TilePreset")]
public class TilePreset : ScriptableObject
{
    public CellType CellType;
    public int Health;
    public Vector2 CellScale;
    
    
    public Sprite Sprite;
}