// シーンの遷移を制御
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // 特定のシーンへ移動
    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ゲーム終了
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
