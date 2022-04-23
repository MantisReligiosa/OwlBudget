using System;

namespace Core.Tools;

public static class DateTimeExtension
{
    private static readonly DateTime UnixEpoch = new(1970, 1, 1);

    public static long ToEpoch(this DateTime dateTime)
    {
        return (long) (dateTime - UnixEpoch).TotalSeconds;
    }

    public static DateTime FromEpoch(long epoch)
    {
        return UnixEpoch.AddSeconds(epoch);
    }
}