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

    public void SetUpPathfinding()
    {
        _pathfinder = new GameObject("AStarPathfinder").AddComponent<AstarPath>();
        
        _pathfinder.data.AddGraph(typeof(GridGraph));
        _pathfinder.data.gridGraph.SetDimensions(_mazeConfig.MazeSize.x, _mazeConfig.MazeSize.y, 1);
        _pathfinder.data.gridGraph.is2D = true;
        _pathfinder.data.gridGraph.center = new Vector3(_mazeConfig.MazeSize.x / 2, _mazeConfig.MazeSize.y / 2,0);
        _pathfinder.data.gridGraph.showNodeConnections = true;
        _pathfinder.data.gridGraph.neighbours = NumNeighbours.Four;
        
        GraphCollision graphCollision = new GraphCollision();
        graphCollision.type = ColliderType.Sphere;
        graphCollision.diameter = 0.9f;
        graphCollision.mask = _mazeConfig.ObstacleMask;
        graphCollision.use2D = true;
        _pathfinder.data.gridGraph.collision = graphCollision;
        
        _pathfinder.logPathResults = PathLog.OnlyErrors;
        _pathfinder.Scan();
    }
}