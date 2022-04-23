using System;

namespace Core.ServiceInterfaces;

public interface IDateTimeProvider
{
    DateTime GetCurrentDatetime();
}