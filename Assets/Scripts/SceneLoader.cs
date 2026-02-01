using UnityEngine.SceneManagement;
public static class SceneLoader
{
    public enum Scene
    {
        StartScreen,
        SidScene,
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        SceneLoader.targetScene = targetScene;

        SceneManager.LoadScene(targetScene.ToString());
    }
}