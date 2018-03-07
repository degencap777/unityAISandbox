using System.Collections.Generic;
using UnityEngine;

public class Logger : SingletonMonoBehaviour<Logger>
{

	[SerializeField]
	private List<LogPermissions> m_permissions = new List<LogPermissions>();

	// --------------------------------------------------------------------------------

	public void Log(string tag, LogLevel logLevel, string message)
	{
#if UNITY_EDITOR
		LogPermissions permissions = null;
		
		for (int i = 0; i < m_permissions.Count; ++i)
		{
			if (string.Compare(tag, m_permissions[i].Tag) == 0)
			{
				permissions = m_permissions[i];
				break;
			}
		}

		if (permissions != null)
		{
			if ((logLevel == LogLevel.Info && permissions.Info) ||
				(logLevel == LogLevel.Warning && permissions.Warning) ||
				(logLevel == LogLevel.Error && permissions.Error))
			{
				LogInternal(tag, logLevel, message);
			}
		}
		else
		{
			m_permissions.Add(new LogPermissions(tag, false, false, false));
		}
#endif
	}

	// --------------------------------------------------------------------------------

	private void LogInternal(string tag, LogLevel logLevel, string message)
	{
		string formattedMessage = string.Format("[{0}] {1}\n", tag, message);
		switch (logLevel)
		{
			case LogLevel.Info:
				Debug.Log(formattedMessage);
				break;
			case LogLevel.Warning:
				Debug.LogWarning(formattedMessage);
				break;
			case LogLevel.Error:
				Debug.LogError(formattedMessage);
				break;
		}
	}

}