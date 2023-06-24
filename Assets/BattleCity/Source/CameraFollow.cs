using System;
using BattleCity.Source.Infrastructure.Services.GameFactory;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private PlayerView _player;

    public void SetTarget(PlayerView player)
    {
        _player = player;
    }

    private void Update()
    {
        if (_player != null)
            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
    }
}