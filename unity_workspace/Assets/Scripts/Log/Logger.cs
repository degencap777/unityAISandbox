using System.Collections.Generic;
using UnityEngine;

public class Logger : SingletonMonoBehaviour<Logger>
{

	private static readonly string k_logFormat = "<b><color=#{0:X2}{1:X2}{2:X2}>{3:0.00}</color></b> <color=#{4:X2}{5:X2}{6:X2}>[{7}] {8}</color>\n";

	// --------------------------------------------------------------------------------

	[SerializeField]
	private Color m_timeColour = Color.white;
	[SerializeField]
	private Color m_infoColour = Color.green;
	[SerializeField]
	private Color m_warningColour = Color.yellow;
	[SerializeField]
	private Color m_errorColour = Color.red;

	[SerializeField]
	private List<LogPermissions> m_permissions = new List<LogPermissions>();

	// --------------------------------------------------------------------------------

	public void Info(object callingClass, string message)
	{
		Info(callingClass.GetType().ToString(), message);
	}
	
	public void Info(string callingClass, string message)
	{
		Log(callingClass, LogLevel.Info, message);
	}

	// --------------------------------------------------------------------------------

	public void Warning(object callingClass, string message)
	{
		Warning(callingClass.GetType().ToString(), message);
	}
	
	public void Warning(string callingClass, string message)
	{
		Log(callingClass, LogLevel.Warning, message);
	}

	// --------------------------------------------------------------------------------

	public void Error(object callingClass, string message)
	{
		Error(callingClass.GetType().ToString(), message);
	}
	
	public void Error(string callingClass, string message)
	{
		Log(callingClass, LogLevel.Error, message);
	}

	// --------------------------------------------------------------------------------

	public void Log(string callingClass, LogLevel logLevel, string message)
	{
#if UNITY_EDITOR
		LogPermissions permissions = null;
		
		for (int i = 0; i < m_permissions.Count; ++i)
		{
			if (string.Compare(callingClass, m_permissions[i].Tag) == 0)
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
				Color colour = m_infoColour;
				switch (logLevel)
				{
					case LogLevel.Warning:
						colour = m_warningColour;
						break;
					case LogLevel.Error:
						colour = m_errorColour;
						break;
				}
				LogInternal(callingClass, logLevel, colour, message);
			}
		}
		else
		{
			m_permissions.Add(new LogPermissions(callingClass, false, false, false));
		}
#endif
	}

	// --------------------------------------------------------------------------------

	private void LogInternal(string callingClass, LogLevel logLevel, Color colour, string message)
	{
		string formattedMessage = string.Format(k_logFormat, 
			(byte)(m_timeColour.r * 255.0f), (byte)(m_timeColour.g * 255.0f), (byte)(m_timeColour.b * 255.0f),
			Time.time,
			(byte)(colour.r * 255.0f), (byte)(colour.g * 255.0f), (byte)(colour.b * 255.0f),
			callingClass, 
			message);

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