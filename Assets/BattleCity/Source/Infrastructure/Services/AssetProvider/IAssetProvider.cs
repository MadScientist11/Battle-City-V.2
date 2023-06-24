using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.GameFactory
{
    public interface IAssetProvider
    {
        T LoadAsset<T>(string path) where T : Object;
    }
}