using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Title, // 타이틀
    Lobby, // 로비
    InGame, // 게임
}

public class SceneLoader : Singleton<SceneLoader>
{
    // 씬 불러오기 함수
    public void LoadScene(SceneType _sceneType)
    {
        // 일반 로그 출력 : 씬 로딩중
        Logger.Log($"{_sceneType} Scene Loading...");

        Time.timeScale = 1;

        // 씬 불러오기
        SceneManager.LoadScene(_sceneType.ToString());
    }

    // 씬 새로고침 함수
    public void ReloadScene()
    {
        // 일반 로그 출력 : 씬 로딩 중
        Logger.Log($"{SceneManager.GetActiveScene().name} Scene Loading...");

        Time.timeScale = 1;

        // 씬 새로고침
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 씬 비동기 불러오기 함수
    public AsyncOperation LoadSceneAsync(SceneType _sceneType)
    {
        // 일반 로그 출력 : 씬 비동기 로딩 중
        Logger.Log($"{_sceneType} Scene Async Loading...");

        Time.timeScale = 1;
        
        // 씬 비동기 불러오기
        return SceneManager.LoadSceneAsync(_sceneType.ToString());
    }
}