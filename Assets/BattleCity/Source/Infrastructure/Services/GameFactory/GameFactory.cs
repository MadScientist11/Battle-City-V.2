using System;
using System.Linq;
using BattleCity.Source.MazeGeneration;
using BattleCity.Source.PlayerLogic;
using UnityEngine;
using UnityEngine.Pool;
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
        private const string ProjectilePath = "Projectile";
        private const string TilePath = "Tile";

        private IAssetProvider _assetProvider;
        private IObjectResolver _instantiator;


        private ObjectPool<Projectile> _pool;
        private MazeConfiguration _mazeConfiguration;

        public GameFactory(IObjectResolver instantiator, IAssetProvider assetProvider, MazeConfiguration mazeConfiguration)
        {
            _mazeConfiguration = mazeConfiguration;
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
        }

        public Projectile GetOrCreateProjectile(GameObject projectileShooter, Vector3 position, Quaternion rotation)
        {
            Projectile projectile = InstancePrefabInjected<Projectile>(ProjectilePath, position, rotation);
            projectile.Initialize(projectileShooter);
            return projectile;
        }

        public MazeView CreateMazeView()
        {
            MazeView mazeView = InstancePrefabInjected<MazeView>(MazeViewPath);
            mazeView.Initialize();
            return mazeView;
        }

        public Player CreatePlayer(Vector3 position) =>
            InstancePrefabInjected<Player>(PlayerPath, position);

        public TileView CreateTile(MazeCell cell, Transform parent)
        {
            Vector3 position = new Vector3(cell.CellCoords.x, cell.CellCoords.y, 0);
            Transform tile = InstancePrefab<Transform>(TilePath, parent, position);
            switch (cell.CellType)
            {
                case CellType.Floor:
                    tile.gameObject.AddComponent<Floor>();
                    break;
                case CellType.Wall:
                    tile.gameObject.AddComponent<Wall>();
                    break;
                case CellType.DestructableWall:
                    tile.gameObject.AddComponent<DestructableWall>();
                    break;
                case CellType.PlayerBase:
                    tile.gameObject.AddComponent<PlayerBase>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cell.CellType), cell.CellType, null);
            }

            if (tile.TryGetComponent(out TileView tileView))
            {
                tileView.Initialize(cell);
                tileView.GetComponent<SpriteRenderer>().sprite = _mazeConfiguration.TilePresets
                    .First(x => x.CellType == tileView.CellType).Sprite;
            }

            return tileView;
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

        private T InstancePrefabInjected<T>(string path, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, position, rotation);
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