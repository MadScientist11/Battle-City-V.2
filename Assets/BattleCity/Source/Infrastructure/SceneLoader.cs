using UnityEngine.SceneManagement;

namespace BattleCity.Source.Infrastructure
{
    public class SceneLoader
    {
        public void Load(string nextScene, System.Action onLoaded)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextScene);

            void OnSceneLoaded(Scene s, LoadSceneMode m)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
                onLoaded?.Invoke();
            }
        }
    }
}