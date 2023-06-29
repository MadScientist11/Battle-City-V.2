﻿using UnityEngine;

namespace BattleCity.Source.PlayerLogic
{
    [CreateAssetMenu(menuName = "Configs", fileName = "Configs/Player")]
    public class PlayerConfiguration : ScriptableObject
    {
        public int Health;
        public float MoveSpeed;
    
    }
}