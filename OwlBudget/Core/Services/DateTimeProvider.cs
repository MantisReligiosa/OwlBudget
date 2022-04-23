using System;
using Core.ServiceInterfaces;

namespace Core.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentDatetime()
    {
        return DateTime.Now;
    }
}