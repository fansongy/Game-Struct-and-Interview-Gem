using UnityEngine;
using System.Collections;
using UnityEditor;

public class UndoOperation : MonoBehaviour 
{
    [MenuItem("MyMenu/DelDisableObject")]
    static void DelObject()
    {
        var objs = Selection.gameObjects;
        foreach (var go in objs)
        {
            for (int i = 0; i < go.transform.childCount; ++i)
            {
                var child = go.transform.GetChild(i).gameObject;
                if (child.activeInHierarchy == false)
                {
                    Undo.DestroyObjectImmediate(child);
                }
            }
        }
    }

    [MenuItem("MyMenu/TransformChange")]
    static void TransformChange()
    {
        var objs = Selection.gameObjects;
        float lineY = 0;
        if (objs.Length > 0)
        {
            lineY = objs[0].transform.position.y;
        }

        foreach (var go in objs)
        {
            var trans = go.transform;
            Undo.RecordObject(trans, "Change Transform");
            trans.position = new Vector3(trans.position.x, lineY, trans.position.z);
        }
    }

}
