using System.Collections;
using System.Collections.Generic;
using BattleCity.Source.Infrastructure.Services;
using UnityEngine;

public interface IRouteSearch : IService
{
    void SetUpPathfinding();
}
