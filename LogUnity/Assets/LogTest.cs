using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;

public class LogTest : MonoBehaviour {

    private void Awake()
    {
        Framework.Logger.SetMask(new Framework.Logger.LoggerLevel[] { Framework.Logger.LoggerLevel.Error, Framework.Logger.LoggerLevel.Warning});
        Framework.Logger.Log(Framework.Logger.LoggerLevel.Warning, "Awake");
    }

    public void Log(string str, string stacktrace)
    {
        Debug.Log("str is " + str);
        Debug.Log("stack trace is " + stacktrace);
    }

    // Use this for initialization
    void Start () {
        Framework.Logger.Log(Framework.Logger.LoggerLevel.Warning, "a");
        Framework.Logger.Log(Framework.Logger.LoggerLevel.Warning, "ab");
        Framework.Logger.Log(Framework.Logger.LoggerLevel.Warning, "abc");
    }

    private int cnt = 0;
    // Update is called once per frame
    void Update () {
        if(Time.frameCount % 10 == 0)
        {
            Framework.Logger.Log(Framework.Logger.LoggerLevel.Warning, cnt++.ToString());
        }
	}
}
