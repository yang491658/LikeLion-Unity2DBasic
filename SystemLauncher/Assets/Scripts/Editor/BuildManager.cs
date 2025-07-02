#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public enum BuildType
{
    DEV,
    TEST,
    REAL
}

public class BuildManager : Editor
{
    public const string DEV_SCRIPTING_DEFINE_SYMBOLS = "DEV_VER";
    public const string REAL_SCRIPTING_DEFINE_SYMBOLS = "";

    private static BuildType m_BuildType = BuildType.DEV;

    [MenuItem("Build/Set AOS DEV Build Settings")]
    public static void SetAOSDEVBuildSettings()
    {
        EditorUserBuildSettings
            .SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        EditorUserBuildSettings.buildAppBundle = false;
        PlayerSettings
            .SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, DEV_SCRIPTING_DEFINE_SYMBOLS);

        m_BuildType = BuildType.DEV;
    }

    [MenuItem("Build/Set AOS TEST Build Settings")]
    public static void SetAOSTESTBuildSettings()
    {
        EditorUserBuildSettings
            .SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        EditorUserBuildSettings.buildAppBundle = true;
        PlayerSettings
            .SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, DEV_SCRIPTING_DEFINE_SYMBOLS);

        m_BuildType = BuildType.TEST;
    }
    [MenuItem("Build/Set AOS REAL Build Settings")]
    public static void SetAOSREALBuildSettings()
    {
        EditorUserBuildSettings
            .SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        EditorUserBuildSettings.buildAppBundle = true;
        PlayerSettings
            .SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, REAL_SCRIPTING_DEFINE_SYMBOLS);

        m_BuildType = BuildType.REAL;
    }

    [MenuItem("Build/Start AOS Build")]
    public static void StartAOSBuild()
    {
        PlayerSettings.Android.keystoreName = "Builds/user.keystore";
        PlayerSettings.Android.keystorePass = "qwer1234";
        PlayerSettings.Android.keyaliasName = "lion";
        PlayerSettings.Android.keyaliasPass = "qwer1234";

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[]
        {
            "Assets/Scenes/Title.unity",
            "Assets/Scenes/Lobby.unity",
            "Assets/Scenes/InGame.unity"
        };
        buildPlayerOptions.target = BuildTarget.Android;
        string fileExtention = string.Empty;
        BuildOptions compressOption = BuildOptions.None;

        switch (m_BuildType)
        {
            case BuildType.DEV:
                fileExtention = "apk";
                compressOption = BuildOptions.CompressWithLz4;
                break;
            case BuildType.TEST:
            case BuildType.REAL:
                fileExtention = "aab";
                compressOption = BuildOptions.CompressWithLz4HC;
                break;
            default:
                break;
        }

        buildPlayerOptions.locationPathName 
            = $"Builds/Launcher_{Application.version}_{DateTime.Now.ToString("yyMMdd_HHmmss")}" +
            $".{fileExtention}";
        buildPlayerOptions.options = compressOption;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        if (summary.result == BuildResult.Succeeded)
        {
            Logger.Log($"Build succeeded. {summary.totalSize} bytes.");
        }
        else if (summary.result == BuildResult.Failed)
        {
            Logger.LogError($"Build failed");
        }
    }
}
#endif