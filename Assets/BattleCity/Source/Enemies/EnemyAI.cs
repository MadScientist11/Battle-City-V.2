using System;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker), typeof(AIPath))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private AIPath _path;
    private Transform _followTarget;

    public void SetFollowTarget(Transform followTarget)
    {
        _followTarget = followTarget;
    }

    private void Update()
    {
        _path.destination = _followTarget.position;
    }
}