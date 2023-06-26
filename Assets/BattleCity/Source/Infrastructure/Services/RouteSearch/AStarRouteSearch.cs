using BattleCity.Source.MazeGeneration;
using Pathfinding;
using UnityEngine;
using VContainer;

public class AStarRouteSearch : IRouteSearch
{
    private AstarPath _pathfinder;
    private MazeConfiguration _mazeConfig;

    [Inject]
    public void Construct(MazeConfiguration mazeConfig)
    {
        _mazeConfig = mazeConfig;
    }

    public void Initialize()
    {
    }

    public void InitializePathfinding()
    {
        _pathfinder = new GameObject("AStarPathfinder").AddComponent<AstarPath>();
        
        _pathfinder.data.AddGraph(typeof(GridGraph));
        _pathfinder.data.gridGraph.SetDimensions(_mazeConfig.MazeSize.x, _mazeConfig.MazeSize.y, 1);
    }
}