﻿using System.Collections.Generic;

namespace BattleCity.Source.Infrastructure.Services.EnemyDirector
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Flatten<T>(this T[,] map) {
            for (int row = 0; row < map.GetLength(0); row++) {
                for (int col = 0; col < map.GetLength(1); col++) {
                    yield return map[row,col];
                }
            }
        }
    }
}