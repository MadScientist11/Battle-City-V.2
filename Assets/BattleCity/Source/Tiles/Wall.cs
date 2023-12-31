﻿using System;
using BattleCity.Source;
using BattleCity.Source.MazeGeneration;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Wall : TileView, ITangible
{
    public Vector2Int Coords { get; set; }
    public Collider2D Collider2D { get; set; }
    public override CellType CellType => CellType.Wall;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer(GameConstants.Layers.ObstacleLayer);
    }

    public override void HealthChanged(int hp)
    {
    }
}