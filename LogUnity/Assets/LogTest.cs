using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class LogTest : MonoBehaviour {

    private void Awake()
    {
        CommonLogger.SetMask(LoggerLevel.Error, LoggerLevel.Warning);
        CommonLogger.Log(LoggerLevel.Warning, "Awake");
        CommonLogger.SetLogColor(LoggerLevel.Error, "red");
        CommonLogger.SetLogColor(LoggerLevel.Warning, "yellow");

    }

    public void Log(string str, string stacktrace)
    {
        Debug.Log("str is " + str);
        Debug.Log("stack trace is " + stacktrace);
    }

    // Use this for initialization
    void Start () {
        CommonLogger.Log(LoggerLevel.Debug, "a");
        CommonLogger.Log(LoggerLevel.Warning, "ab");
        CommonLogger.Log(LoggerLevel.Warning, "abc");
    }

    private int cnt = 0;
    // Update is called once per frame
    void Update () {
        if(Time.frameCount % 10 == 0)
        {
            CommonLogger.Log(LoggerLevel.Warning, cnt++.ToString());
        }
	}
}
