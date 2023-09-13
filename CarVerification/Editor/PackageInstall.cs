using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using System.Linq;

public class PackageInstall : MonoBehaviour
{
    [MenuItem("INVENTIS/Add Package")]
    private static void CheckAndInstallPackage()
    {
        var packageName = "com.unity.editorcoroutines";
        var packageVersion = "1.0.0";

        var request = Client.List(true, true);
        while (!request.IsCompleted)
        {
            // 패키지 목록을 가져오는 동안 기다립니다.
        }

        if (request.Status == StatusCode.Success)
        {
            if (!request.Result.Any(p => p.name == packageName))
            {
                Client.Add($"{packageName}@{packageVersion}");
            }
            else
            {
                Debug.Log("Package is already installed.");
            }
        }
        else
        {
            Debug.LogError("Failed to list packages.");
        }
    }
}
