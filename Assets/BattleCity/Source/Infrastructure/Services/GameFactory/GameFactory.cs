using System;
using System.Linq;
using BattleCity.Source.Enemies;
using BattleCity.Source.Infrastructure.Services.MazeService;
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
        private const string EnemyPath = "Enemy";

        private IAssetProvider _assetProvider;
        private IObjectResolver _instantiator;


        private ObjectPool<Projectile> _pool;
        private MazeConfiguration _mazeConfiguration;
        private IMazeManager _mazeManager;

        public GameFactory(IObjectResolver instantiator, IAssetProvider assetProvider, IMazeManager mazeManager,
            MazeConfiguration mazeConfiguration)
        {
            _mazeManager = mazeManager;
            _mazeConfiguration = mazeConfiguration;
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public void Initialize()
        {
        }

        //TODO: Implement pool
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
            Vector3 position = _mazeManager.GetCellPosition(cell);
            
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
                TilePreset tilePreset = _mazeConfiguration.TilePresets
                    .First(x => x.CellType == tileView.CellType);
                tileView.GetComponent<SpriteRenderer>().sprite = tilePreset.Sprite;
                tileView.transform.localScale = cell.CellScale;
            }

            return tileView;
        }

        public Enemy CreateEnemy(Vector2Int cellCoords)
        {
            MazeCell mazeCell = _mazeManager.Maze[cellCoords.x, cellCoords.y];
            Vector3 position = new Vector3(mazeCell.CellCoords.x + mazeCell.CellScale.x/2, mazeCell.CellCoords.y + mazeCell.CellScale.y/2,
                0);
            return InstancePrefabInjected<Enemy>(EnemyPath, new Vector3(position.x, position.y, -1));
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