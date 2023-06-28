using UnityEngine;

namespace BattleCity.Source.MazeGeneration
{
    [CreateAssetMenu(menuName = "Maze", fileName = "MazeConfig")]
    public class MazeConfiguration : ScriptableObject
    {
        public Vector2Int MazeSize;
        [Range(1,20)] public int PathSpread;
        [Range(0.0f, 1.0f)] public float DestructableWallProbability;

        public TilePreset[] TilePresets;
        public GameObject TilePrefab;
    }
}