using System;
using BattleCity.Source;
using BattleCity.Source.Enemies;
using BattleCity.Source.MazeGeneration;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class DestructableWall : TileView, ITangible, IDamageable
{
    public Vector2Int TileIndices { get; set; }
    public Collider2D Collider2D { get; set; }
    public override CellType CellType => CellType.DestructableWall;

    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
        gameObject.layer = LayerMask.NameToLayer(GameConstants.Layers.DestructableLayer);
    }

    public override void HealthChanged(int hp)
    {
        Debug.Log(hp);
    }

    public void TakeDamage(int amount)
    {
        Cell.TakeDamage(amount);
    }

 
   
}