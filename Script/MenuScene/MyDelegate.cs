using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDelegate : MonoBehaviour {

    public delegate void SceneDelegate(string sceneName, int n);   //定义 委托名为LogDelegate,带一个string参数的 委托类型

    public static SceneDelegate SceneEvent;             //声明委托对象,委托实例为LogEvent  

    public static void OnSceneEvent(string sceneName, int n)       //可以直接 MyDelegate.LogEvent("")调用委托，这么写方便管理，还可以扩展这个方法;
    {
        if (SceneEvent != null) {
            SceneEvent(sceneName,n);
        }
    }

}
