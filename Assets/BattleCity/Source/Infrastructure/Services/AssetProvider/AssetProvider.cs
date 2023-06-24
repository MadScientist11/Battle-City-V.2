using UnityEngine;

namespace BattleCity.Source.Infrastructure.Services.GameFactory
{
    public class AssetProvider : IAssetProvider
    {
        public void Initialize()
        {
        }

        public T LoadAsset<T>(string path) where T : Object
        {
            T asset = Resources.Load<T>(path);

            if (asset == null)
            {
                Debug.LogError($"The asset at path \"{path}\" doesn't exist");
            }
            return asset;
        }
    }
}