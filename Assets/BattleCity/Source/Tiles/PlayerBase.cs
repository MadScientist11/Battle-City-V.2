using System;
using BattleCity.Source.Enemies;
using BattleCity.Source.MazeGeneration;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class PlayerBase : TileView, ITangible, IDamageable
{
    public Vector2Int Coords { get; set; }
    public Collider2D Collider2D { get; set; }
    public override CellType CellType => CellType.PlayerBase;
    public override void SetHealth(int hp)
    {
        
    }

    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
        gameObject.layer = LayerMask.NameToLayer("InteractableTile");

        Collider2D.isTrigger = true;
    }

    public void TakeDamage(int amount)
    {
    }
}

public interface ITangible
{
    public Collider2D Collider2D { get; set; }
}