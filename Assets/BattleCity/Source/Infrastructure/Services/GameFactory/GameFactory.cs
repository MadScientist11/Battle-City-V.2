using System;
using BattleCity.Source.MazeGeneration;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace BattleCity.Source.Infrastructure.Services.GameFactory
{
    public interface INeedPreEnableSetup<T>
    {
        void PreEnableSetup(T param);
    }

    public class GameFactory : IGameFactory
    {
        private const string MazeViewPath = "MazeView";
        private const string PlayerPath = "Player";
        private IAssetProvider _assetProvider;
        private IObjectResolver _instantiator;

        public GameFactory(IObjectResolver instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
        }

        public MazeView CreateMazeView()
        {
            MazeView mazeView = InstancePrefabInjected<MazeView>(MazeViewPath);
            mazeView.Initialize();
            return mazeView;
        }

        public PlayerView CreatePlayer(Vector3 position) => 
            InstancePrefabInjected<PlayerView>(PlayerPath, position);

        public void CreateMazeCell(CellType cellType, Transform parent, Vector3 position)
        {
            switch (cellType)
            {
                case CellType.Floor:
                    InstancePrefab<Transform>("Floor", parent, position);
                    break;
                case CellType.Wall:
                    InstancePrefab<Transform>("Wall", parent, position);
                    break;
                case CellType.DestructableWall:
                    InstancePrefab<Transform>("DestructableWall", parent, position);
                    break;
                case CellType.PlayerBase:
                    InstancePrefab<Transform>("PlayerBase", parent, position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellType), cellType, null);
            }
          
        }

        private T InstancePrefab<T>(string path) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            return Object.Instantiate(asset);
        }

        private T InstancePrefab<T>(string path, Transform parent, Vector3 position) where T : Object
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            return Object.Instantiate(asset, position, Quaternion.identity, parent);
        }

        private T InstancePrefabInjected<T>(string path) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset);
            SceneManager.MoveGameObjectToScene(instance.gameObject, SceneManager.GetActiveScene());
            instance.gameObject.SetActive(true);
            return instance;
        }
        private T InstancePrefabInjected<T>(string path, Vector3 position) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, position, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(instance.gameObject, SceneManager.GetActiveScene());
            instance.gameObject.SetActive(true);
            return instance;
        }

        private T InstancePrefabInjected<T>(string path, Transform parent) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, parent);
            SceneManager.MoveGameObjectToScene(instance.gameObject, SceneManager.GetActiveScene());
            instance.gameObject.SetActive(true);
            return instance;
        }

        private T InstancePrefabInjected<T>(string path, Transform parent, Action<T> initInstance)
            where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, parent);
            SceneManager.MoveGameObjectToScene(instance.gameObject, SceneManager.GetActiveScene());
            initInstance.Invoke(instance);
            instance.gameObject.SetActive(true);
            return instance;
        }
    }
}