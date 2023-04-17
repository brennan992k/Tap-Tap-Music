using System;

public class TimeUtils
{
	public static int Now()
	{
		return (int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
	}

	public static int NowMinutes()
	{
		return (int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMinutes;
	}

	public static string Decorate(int value)
	{
		if (value < 10)
		{
			return "0" + value;
		}
		return string.Empty + value;
	}
}
