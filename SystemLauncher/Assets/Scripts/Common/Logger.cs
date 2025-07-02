using System.Diagnostics;

public static class Logger
{
    // 일반 로그 출력 함수 : DEV_VER 조건부 컴파일에서만 동작
    [Conditional("DEV_VER")]
    public static void Log(string msg)
    {
        UnityEngine.Debug.LogFormat("[{0}] {1}",
            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }

    // 경고 로그 출력 함수 : DEV_VER 조건부 컴파일에서만 동작 
    [Conditional("DEV_VER")]
    public static void LogWarning(string msg)
    {
        UnityEngine.Debug.LogWarningFormat("[{0}] {1}",
            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }

    // 오류 로그 출력 함수 : 항상 동작
    public static void LogError(string msg)
    {
        UnityEngine.Debug.LogErrorFormat("[{0}] {1}",
            System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), msg);
    }
}