using System;
using BattleCity.Source.Infrastructure.Services.GameFactory;
using BattleCity.Source.PlayerLogic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player _player;

    public void SetTarget(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        if (_player != null)
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
    }
}