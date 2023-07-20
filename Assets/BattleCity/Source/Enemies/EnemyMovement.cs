using System.Collections.Generic;
using BattleCity.Source.Enemies;
using BattleCity.Source.Infrastructure.Services.MazeService;
using Pathfinding;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(Seeker), typeof(AIPath))]
public class EnemyMovement : MonoBehaviour
{
    public bool IsInFrontOfTarget { get; private set; }

    [SerializeField] private AIPath _path;
    [SerializeField] private LayerMask _stopMask;

    private Transform _followTarget;
    private RaycastHit2D[] _results;

    private void Start()
    {
        _results = new RaycastHit2D[1];
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    public void SetFollowTarget(Transform followTarget)
    {
        _followTarget = followTarget;
    }

    private void Update()
    {
        if (Physics2D.RaycastNonAlloc(transform.position, transform.up, _results, 2) > 0)
        {
            if (_results[0].transform.TryGetComponent(out TileView tile) && _results[0].transform.TryGetComponent(out IDamageable _))
            {
                List<Vector3> buffer = new List<Vector3>();
                _path.GetRemainingPath(buffer, out bool stale);
                foreach (var pos in buffer)
                {
                    if (tile.transform.position == pos)
                    {
                        Vector2 vectorToTarget = _results[0].transform.position - transform.position;
                        float look = Vector2.Dot(vectorToTarget.normalized, transform.up);
                        IsInFrontOfTarget = Mathf.Abs(look) > 0.85f || vectorToTarget.magnitude <= .75f;
                    }
                }
            }
            else
            {
                IsInFrontOfTarget = false;
            }
        }
        else
        {
            IsInFrontOfTarget = false;
        }

        _path.isStopped = IsInFrontOfTarget;
        
        if (!IsInFrontOfTarget)
            _path.destination = new Vector3(_followTarget.position.x, _followTarget.position.y, -1);
    }
}