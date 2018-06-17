using UnityEngine;
using System.Collections;
using UnityEditor;

public class FileMove : AssetModificationProcessor
{
    private static string prefabPath = "Assets/Chapter6/3/Res/TestPrefab.prefab";

    public static AssetMoveResult OnWillMoveAsset(string oldPath,string newPath)
    {
        AssetMoveResult result = AssetMoveResult.DidNotMove;
        if (oldPath == prefabPath)
        {
            result = AssetMoveResult.FailedMove;
            EditorUtility.DisplayDialog("Attention", "You shouldn't move this asset","Ok");
        }
        return result;
    }
}
