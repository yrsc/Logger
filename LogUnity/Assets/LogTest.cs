using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class LogTest : MonoBehaviour {

    private void Awake()
    {
        CommonLogger.SetMask(LoggerLevel.Error, LoggerLevel.Warning, LoggerLevel.Debug);
        var path = Application.persistentDataPath + "/ " + System.DateTime.Now.ToShortTimeString();
        CommonLogger.Log(LoggerLevel.Debug, "path is " + path);
        CommonLogger.SetWritePath(path);
        CommonLogger.Log(LoggerLevel.Warning, "Awake", LogColor.blue);
    }
    // Use this for initialization
    void Start () {
        DoTest();
        CommonLogger.Log(LoggerLevel.Warning, "ab",LogColor.red);
        CommonLogger.Log(LoggerLevel.Debug, "abc", new LogColor(1, 1f, 0.0f));
    }

    void DoTest()
    {
        CommonLogger.LogAndWrite(LoggerLevel.Debug, "DoTest ", true, new LogColor(1, 0f, 0.0f));
    }

    private int cnt = 0;
    // Update is called once per frame
    void Update () {
        if(Time.frameCount % 10 == 0)
        {
            CommonLogger.LogAndWrite(LoggerLevel.Warning, cnt++.ToString());
        }
	}
}
