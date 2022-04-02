using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Util
{
    public static string FormatTime(float time)
    {
        return TimeSpan.FromSeconds(time).ToString("mm\\:ss\\.ff");
    }
}