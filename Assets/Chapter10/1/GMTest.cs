using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class GMTest : MonoBehaviour
{
    string inputText = "";
    void OnGUI()
    {
        inputText = GUILayout.TextField(inputText, GUILayout.Height(30), GUILayout.Width(200));


        if (GUILayout.Button("Submit", GUILayout.Width(100), GUILayout.Height(50)))
        {
            Debug.Log(GMModule.Instance.Call(inputText));
        }
    }
}

public class GMModule
{

	#region Public Attributes

	#endregion

	#region Private Attributes
    private static GMModule _instance;
    public static GMModule Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GMModule();
                _instance.Init();

            }
            return _instance;
        }
    }

    private Dictionary<string, MethodInfo> m_methods = new Dictionary<string, MethodInfo>();
	#endregion

	#region Public Methods
    public void Init()
    {
        m_methods.Clear();

        System.Type type = typeof(GMModule);
        var methods =type.GetMethods();
        foreach(var each in methods)
        {
            var attribute = each.GetCustomAttributes(typeof(GMCommondAttribute), false);
            if(attribute != null && attribute.Length>0)
            {
                GMCommondAttribute gmc = attribute[0] as GMCommondAttribute;
                m_methods.Add(gmc.Cmd, each);
            }
        }

    }

    public string Call(string input)
    {
        var tmpStr = input.Split(' ');
        if (m_methods.ContainsKey(tmpStr[0]))
        {
            List<string> param = new List<string>();
            for (int i = 1; i < tmpStr.Length; ++i)
            {
                param.Add(tmpStr[i]);
            }

            var method = m_methods[tmpStr[0]];
            var info = method.GetCustomAttributes(typeof(GMCommondAttribute), false)[0] as GMCommondAttribute;

            if (param.Count != info.ParamNum)
            {
                return "Usage: "+info.Usage;
            }
            else
            {
                return m_methods[tmpStr[0]].Invoke(this, new object[] { param.ToArray() }) as string;
            }
        }
        else
        {
            return "Commond Not Found!";
        }
    }


    [GMCommond("userId", 0, "userId | 显示玩家ID")]
    public string help(string[] args)
    {
        int userId = 666;

        //Query logic to get ID

        return "User id is:" + userId;
    }

    [GMCommond("lvUp", 1, "lvUp 80 | 升级到xx ")]
    public string levelUp(string[] args)
    {
        //ask server to level up

        return "level up to " + args[0];
    }

    #endregion
}


[AttributeUsage(AttributeTargets.Method)]
public class GMCommondAttribute : Attribute
{
    public string Cmd;
    public int ParamNum;
    public string Usage;
    public GMCommondAttribute(string cmd,int paramNum, string usage)
    {
        Cmd = cmd;
        ParamNum = paramNum;
        Usage = usage;
    }
}