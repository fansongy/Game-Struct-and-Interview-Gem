using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CreateTool : MonoBehaviour
{
    [MenuItem("GameObject/MyUI/Button", false, 0)]
    public static void MyBtn()
    {
        GameObject buttonRoot = new GameObject("Button");

        GameObject childText = new GameObject("Text");
        childText.transform.SetParent(buttonRoot.transform, false);

        Image image = buttonRoot.AddComponent<Image>();
        image.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Chapter12/2/BtnTexture.png");
        image.SetNativeSize();

        Button bt = buttonRoot.AddComponent<Button>();

        Text text = childText.AddComponent<Text>();
        text.text = "Button";
        text.alignment = TextAnchor.MiddleCenter;
        text.color = new Color(0, 0, 0);
        text.transform.SetParent(buttonRoot.transform, text);
        text.raycastTarget = false;

        RectTransform textRectTransform = childText.GetComponent<RectTransform>();
        textRectTransform.anchorMin = Vector2.zero;
        textRectTransform.anchorMax = Vector2.one;
        textRectTransform.sizeDelta = Vector2.zero;
        
        if (Selection.gameObjects.Length > 0)
        {
            buttonRoot.transform.SetParent(Selection.gameObjects[0].transform, false);
        }

        // Make Button Selected
        Selection.objects = new Object[] { buttonRoot };
    }

}
