using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Framework
{
	public class LoggerHelper : MonoBehaviour
	{
		public bool Info = true;
		public bool Debug = true;
		public bool Warning = true;
		public bool Error = true;
		public bool Exception = true;

		void Awake ()
		{
			List<LoggerLevel> loggerLevels = new List<LoggerLevel> ();
			if (Info)
			{
				loggerLevels.Add (LoggerLevel.Info);
			}
			if (Debug)
			{
				loggerLevels.Add (LoggerLevel.Debug);
			}
			if (Warning)
			{
				loggerLevels.Add (LoggerLevel.Warning);
			}
			if (Error)
			{
				loggerLevels.Add (LoggerLevel.Error);
			}
			if (Exception)
			{
				loggerLevels.Add (LoggerLevel.Exception);
			}
			CommonLogger.SetMask (loggerLevels.ToArray ());
			CommonLogger.SetWritePath (Application.persistentDataPath);
		}

		// Update is called once per frame
		void Update ()
		{ }
	}
}