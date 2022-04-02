using System;

public static class Util
{
    public static string FormatTime(float time)
    {
        return TimeSpan.FromSeconds(time).ToString("mm\\:ss\\.ff");
    }
}