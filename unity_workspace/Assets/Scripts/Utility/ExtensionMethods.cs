

public static class ExtensionMethods
{

	// Logger -------------------------------------------------------------------------
	// --------------------------------------------------------------------------------

	public static void LogInfo<T>(this T logger, string message)
	{
#if UNITY_EDITOR
		if (Logger.Instance != null)
		{
			Logger.Instance.Log(logger.GetType().ToString(), LogLevel.Info, message);
		}
#endif
	}

	// --------------------------------------------------------------------------------

	public static void LogWarning<T>(this T logger, string message)
	{
#if UNITY_EDITOR
		if (Logger.Instance != null)
		{
			Logger.Instance.Log(logger.GetType().ToString(), LogLevel.Warning, message);
		}
#endif
	}

	// --------------------------------------------------------------------------------

	public static void LogError<T>(this T logger, string message)
	{
#if UNITY_EDITOR
		if (Logger.Instance != null)
		{
			Logger.Instance.Log(logger.GetType().ToString(), LogLevel.Error, message);
		}
#endif
	}

}