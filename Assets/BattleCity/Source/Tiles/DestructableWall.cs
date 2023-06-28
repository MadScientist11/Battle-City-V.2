using System;
using BattleCity.Source.Enemies;
using BattleCity.Source.MazeGeneration;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class DestructableWall : TileView, ITangible, IDamageable
{
    public Vector2Int TileIndices { get; set; }
    public Collider2D Collider2D { get; set; }
    public override CellType CellType => CellType.DestructableWall;
    public override void SetHealth(int hp)
    {
        Debug.Log("w");
    }


    private void Start()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    public void TakeDamage(int amount)
    {
        Cell.SetHealth(Cell.Health - amount);
    }

 
   
}