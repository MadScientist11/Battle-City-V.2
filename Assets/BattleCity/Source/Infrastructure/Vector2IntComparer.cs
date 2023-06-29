using System.Collections.Generic;
using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.EnemyDirector
{
    public class Vector2IntComparer : IComparer<Vector2Int>
    {
        public int Compare(Vector2Int x, Vector2Int y)
        {
            int xComparison = x.x.CompareTo(y.x);
            if (xComparison != 0) return xComparison;
            return x.y.CompareTo(y.y);
        }
    }
}